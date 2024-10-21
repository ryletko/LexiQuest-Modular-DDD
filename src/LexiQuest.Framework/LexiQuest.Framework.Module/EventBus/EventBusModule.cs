using System.Reflection;
using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.Context;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.Framework.Infrastructure.DataAccess;
using LexiQuest.Framework.Infrastructure.Dependencies;
using LexiQuest.Framework.Infrastructure.EventBus;
using LexiQuest.Framework.Module.Config;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Utils.Core;
using MassTransitBus = LexiQuest.Framework.Infrastructure.EventBus.MassTransitBus;

namespace LexiQuest.Framework.Module.EventBus;

internal record EventBusModuleParameters(IServiceCollection Services,
                                         MessageBrokerSettings Settings,
                                         ModuleContext ModuleContext,
                                         IModuleStartupConfiguration ModuleConfig,
                                         ICompositionRoot CompositionRoot);

internal class EventBusModule<T>(EventBusModuleParameters parameters) : IEventBusModule where T : BaseDbContext
{
    public IEventBusModule Register()
    {
        parameters.Services.AddMassTransit(
            x =>
            {
                // x.AddMediator(cfg =>
                //               {
                //                   var filter = (Type t) => typeof(IInternalMessageHandler).IsAssignableFrom(t) || t.GetCustomAttribute(typeof(InternalMessageHandlerAttribute)) != null;
                //                   cfg.AddConsumersFromAssemblyContaining(parameters.ModuleContext.ApplicationAssembly, filter);
                //               });
                x.AddPublishMessageScheduler(); // это для requests в саге
                x.UsingRabbitMq((ctx, cfg) =>
                                {
                                    cfg.Host(new Uri(parameters.Settings.Host),
                                             h =>
                                             {
                                                 h.Username(parameters.Settings.Username);
                                                 h.Password(parameters.Settings.Password);
                                             });
                                    cfg.UseInMemoryOutbox(ctx);
                                    cfg.ConfigureEndpoints(ctx);
                                    cfg.UsePublishMessageScheduler();
                                });

                x.AddConfigureEndpointsCallback((context, name, cfg) =>
                                                {
                                                    //if LiveServer {
                                                    cfg.UseMessageRetry(r => r.Intervals(500)); //, 500, 1000, 5000, 10000));
                                                    // }
                                                    cfg.UseEntityFrameworkOutbox<T>(context);
                                                });

                var filter = parameters.ModuleConfig.OverwriteFilter != null
                    ? parameters.ModuleConfig.OverwriteFilter.Filter
                    : t => typeof(IEventBusMessageHandler).IsAssignableFrom(t) || t.GetCustomAttribute<EventBusMessageHandlerAttribute>() != null;

                x.AddConsumersFromAssemblyContaining(parameters.ModuleContext.ApplicationAssembly, filter);
                AddSagas(x, parameters.ModuleContext.ApplicationAssembly);

                x.AddEntityFrameworkOutbox<T>(o =>
                                              {
                                                  o.UsePostgres(false);
                                                  o.UseBusOutbox();
                                                  o.DuplicateDetectionWindow = TimeSpan.FromMinutes(30);
                                              });
                MessageCorrelation.UseCorrelationId<IContextedMessage>(c => c.MessageContext.CorrelationId);

                parameters.ModuleConfig.AdditionalMassTransitConfg?.Apply(configmt => configmt(x));
            });

        parameters.Services.AddScoped<IEventBus, MassTransitBus>();
        parameters.Services.AddScoped<IMediatorEventBus, MediatorEventBus>();

        return this;
    }

    private void AddSagas(IBusRegistrationConfigurator configurator, Assembly assembly,
                          Func<Type, bool>? filter = null)
    {
        var types = assembly.GetTypes().Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                                                   t.BaseType.GetGenericTypeDefinition() == typeof(MassTransitStateMachine<>));
        if (filter != null)
            types = types.Where(filter);

        foreach (var t in types)
        {
            var sagaType = t.BaseType.GetGenericArguments()[0];
            var addedSaga = configurator.AddSagaStateMachine(t);
            // var sagaConfigMethod = this.GetType().GetMethod(nameof(GetSagaConfig), BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(sagaType);
            // var efConfigAction = sagaConfigMethod.Invoke(this, null);

            // var efExtStaticType = typeof(EntityFrameworkCoreSagaRepositoryRegistrationExtensions);
            // var efConfigStaticMethod = efExtStaticType.GetMethods(BindingFlags.Static | BindingFlags.Public)
            //                                           .FirstOrDefault(x =>
            //                                                           {
            //                                                               var parameters = x.GetParameters();
            //                                                               return x.Name == nameof(EntityFrameworkCoreSagaRepositoryRegistrationExtensions.EntityFrameworkRepository) &&
            //                                                                      x.GetGenericArguments().Length == 1 &&
            //                                                                      parameters.Length == 2 &&
            //                                                                      parameters[0].ParameterType.GetGenericTypeDefinition() == typeof(ISagaRegistrationConfigurator<ISaga>).GetGenericTypeDefinition() &&
            //                                                                      parameters[1].ParameterType.GetGenericTypeDefinition() == typeof(Action<IEntityFrameworkSagaRepositoryConfigurator<ISaga>>).GetGenericTypeDefinition();
            //                                                           });
            this.GetType()
                .GetMethod(nameof(EntityFrameworkRepository), BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(sagaType)
                .Invoke(this, [addedSaga]);
            // efConfigStaticMethodGeneric.Invoke(null, [addedSaga, efConfigAction]);
        }
    }

    // private Action<IEntityFrameworkSagaRepositoryConfigurator<TSaga>> GetSagaConfig<TSaga>() where TSaga : class, ISaga
    // {
    //     return c =>
    //            {
    //                c.ExistingDbContext<T>();
    //                c.UsePostgres(parameters.ModuleContext.SchemaName);
    //            };
    // }

    private ISagaRegistrationConfigurator<TSaga> EntityFrameworkRepository<TSaga>(ISagaRegistrationConfigurator<TSaga> configurator)
        where TSaga : class, ISaga
    {
        return configurator.EntityFrameworkRepository(c =>
                                                      {
                                                          c.ExistingDbContext<T>();
                                                          c.UsePostgres(parameters.ModuleContext.SchemaName);
                                                      });
    }

    public async Task Start()
    {
        await using (var scope = parameters.CompositionRoot.BeginScope())
        {
            LogContext.ConfigureCurrentLogContext(scope.GetLoggerFactory());
            var bus = scope.GetBusControl();
            await bus.StartAsync().ConfigureAwait(false);
        }
    }

    public async Task Stop()
    {
        await using (var scope = parameters.CompositionRoot.BeginScope())
        {
            var bus = scope.GetBusControl();
            await bus.StopAsync().ConfigureAwait(false);
        }
    }
}

internal interface IEventBusModule
{
    Task Start();
    Task Stop();
    IEventBusModule Register();
}