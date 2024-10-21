using Autofac;
using LexiQuest.Framework.Application.DataAccess;
using LexiQuest.Framework.Application.Messages.InternalProcessing;
using LexiQuest.Framework.Infrastructure.DataAccess;
using LexiQuest.Framework.Infrastructure.InternalProcessing.DomainEventsDispatching;
using LexiQuest.Framework.Infrastructure.InternalProcessing.InternalCommands;
using LexiQuest.Framework.Infrastructure.InternalProcessing.Outbox;
using LexiQuest.Framework.Module.Config;

namespace LexiQuest.Framework.Module.InternalProcessing;

internal class ProcessingModule(ModuleContext moduleContext) : Autofac.Module
{
    // private static readonly Type[] CommandHandlerDecorators =
    // [
    //     typeof(UnitOfWorkCommandHandlerDecorator<>),
    //     typeof(ValidationCommandHandlerDecorator<>),
    //     typeof(LoggingCommandHandlerDecorator<>)
    // ];
    //
    // private static readonly Type[] CommandHandlersWithResultDecorators =
    // [
    //     typeof(UnitOfWorkCommandHandlerDecorator<,>),
    //     typeof(ValidationCommandHandlerDecorator<,>),
    //     typeof(LoggingCommandHandlerDecorator<,>)
    // ];
    //
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DomainEventsDispatcher>()
               .As<IDomainEventsDispatcher>()
               .InstancePerLifetimeScope();

        builder.RegisterType<DomainEventsAccessor>()
               .As<IDomainEventsAccessor>()
               .InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>()
               .As<IUnitOfWork>()
               .InstancePerLifetimeScope();

        builder.RegisterType<InternalCommandsScheduler>()
               .As<IInternalCommandsScheduler>()
               .InstancePerLifetimeScope();

        builder.RegisterType<ProcessOutboxCommandHandler>();

        builder.RegisterType<OutboxErrorNotifier>()
               .As<IOutboxErrorNotifier>()
               .InstancePerLifetimeScope();

        // builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerDecorator<>), typeof(ICommandHandler<>));
        // builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>), typeof(ICommandHandler<,>));
        // builder.RegisterGenericDecorator(typeof(ValidationCommandHandlerDecorator<>), typeof(ICommandHandler<>));
        // builder.RegisterGenericDecorator(typeof(ValidationCommandHandlerWithResultDecorator<,>), typeof(ICommandHandler<,>));
        // builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerDecorator<>), typeof(ICommandHandler<>));
        // builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerWithResultDecorator<,>), typeof(ICommandHandler<,>));
        // builder.RegisterGenericDecorator(typeof(DomainEventsDispatcherNotificationHandlerDecorator<>), typeof(IDomainEventHandler<>));

        // RegisterCommanHandlersDecorators();
        // RegisterCommanHandlersWithResultDecorators();
        // RegisterDomainEventHandlersDecorators();

        // Register decorators


        // builder.RegisterAssemblyTypes(moduleContext.ApplicationAssembly)
        //        .AsClosedTypesOf(typeof(ICommandHandler<>))
        //        .AsImplementedInterfaces()
        //        .InstancePerLifetimeScope();


        // builder.RegisterGeneric(typeof(TestDecorator<>)).AsSelf();
        //
        // builder.RegisterAssemblyTypes(moduleContext.ApplicationAssembly)
        //        .AsClosedTypesOf(typeof(BaseCommandHandler<>))
        //        .OnActivating(e =>
        //                      {
        //                          var instanceType = e.Instance.GetType();
        //                          foreach (var baseType in ScanBaseTypes(instanceType))
        //                          {
        //                              if (baseType.GetGenericTypeDefinition() == typeof(BaseCommandHandler<>))
        //                              {
        //                                  var genericParameters = baseType.GetGenericArguments();
        //                                  var instance = e.Instance;
        //                                  var decoratorClosedType = typeof(TestDecorator<>).MakeGenericType(genericParameters);
        //                                  var decorator = e.Context.Resolve(decoratorClosedType);
        //                                  var propertyInfo = decoratorClosedType.GetProperty(nameof(TestDecorator<DummyCommand>.Decorated));
        //                                  propertyInfo.SetValue(decorator, instance);
        //                                  e.ReplaceInstance(decorator);
        //                                  break;
        //                              }
        //                          }
        //                      });
    }
}