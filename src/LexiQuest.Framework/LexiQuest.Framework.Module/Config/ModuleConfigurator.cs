using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using LexiQuest.Framework.Infrastructure.DataAccess;
using LexiQuest.Framework.Infrastructure.DataAccess.SqlConnection;
using LexiQuest.Framework.Infrastructure.Dependencies;
using LexiQuest.Framework.Infrastructure.EventBus;
using LexiQuest.Framework.Infrastructure.InternalProcessing;
using LexiQuest.Framework.Infrastructure.InternalProcessing.DomainEventsDispatching;
using LexiQuest.Framework.Infrastructure.InternalProcessing.Outbox;
using LexiQuest.Framework.Infrastructure.InternalProcessing.Quartz;
using LexiQuest.Framework.Module.DataAccess;
using LexiQuest.Framework.Module.EventBus;
using LexiQuest.Framework.Module.InternalProcessing;
using LexiQuest.Framework.Module.Logging;
using LexiQuest.Framework.Module.Mediator;
using LexiQuest.Framework.Module.Quartz;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using Utils.Core;
using ILogger = Serilog.ILogger;

namespace LexiQuest.Framework.Module.Config;

internal interface IServicesModuleConfigurator
{
    IServicesModuleConfigurator RegisterHttpClient();
    IServicesModuleConfigurator RegisterEventBus();
    IServicesModuleConfigurator RegisterMediator();
    IAutoFacBuilderConfigurator ToAutofac();
}

internal interface IAutoFacBuilderConfigurator
{
    IAutoFacBuilderConfiguratorWithLoggerFactory RegisterLoggerFactory();
    IAutoFacBuilderConfigurator RegisterHandlerDecorators();
    IAutoFacBuilderConfigurator RegisterAssembliesServices();
    IFinalizedConfig FinalizeConfig();
}

internal interface IAutoFacBuilderConfiguratorWithLoggerFactory : IAutoFacBuilderConfigurator
{
    IAutoFacBuilderConfiguratorWithDataAccess RegisterDataAccess();
}

internal interface IAutoFacBuilderConfiguratorWithDataAccess : IAutoFacBuilderConfiguratorWithLoggerFactory
{
    IAutoFacBuilderConfiguratorWithDataAccess RegisterInternalProcessing();
}

internal interface IFinalizedConfig
{
    IContainer Container { get; }
    IQuartz Quartz { get; }
    IEventBusModule EventBusModule { get; }
    ILogger ModuleLogger { get; }
    ModuleContext ModuleContext { get; }
    IModuleStartupConfiguration Config { get; }
}

internal class ModuleConfigurator : IServicesModuleConfigurator, IAutoFacBuilderConfigurator, IAutoFacBuilderConfiguratorWithLoggerFactory, IAutoFacBuilderConfiguratorWithDataAccess, IFinalizedConfig
{
    private readonly IModuleStartupConfiguration _config;
    private readonly ILogger _moduleLogger;
    private readonly ModuleContext _moduleContext;
    private readonly Type _dbContextType;
    private readonly MessageBrokerSettings _messageBrokerSettings;
    private readonly string _connectionString;
    private readonly long? _internalProcessingPoolingInterval;
    private readonly IServiceCollection _services;
    private IEventBusModule _eventBusModule;
    private ContainerBuilder _builder;
    private ILoggerFactory _loggerFactory;
    private IContainer _container;
    private IQuartz _quartz = new DummyQuartz();
    private HandlerDecorators _handlerDecorators;
    private ICompositionRoot _compositionRoot;

    private ModuleConfigurator(ServiceCollection services, IModuleStartupConfiguration config, ILogger moduleLogger, ModuleContext moduleContext, Type dbContextType, MessageBrokerSettings messageBrokerSettings,
                               string connectionString, long? internalProcessingPoolingInterval)
    {
        _services                          = services;
        _config                            = config;
        _moduleLogger                      = moduleLogger;
        _moduleContext                     = moduleContext;
        _dbContextType                     = dbContextType;
        _messageBrokerSettings             = messageBrokerSettings;
        _connectionString                  = connectionString;
        _internalProcessingPoolingInterval = internalProcessingPoolingInterval;
        _compositionRoot                   = new CompositionRoot(() => _container);
    }

    public IServicesModuleConfigurator RegisterHttpClient()
    {
        _services.AddHttpClient();
        return this;
    }

    public IServicesModuleConfigurator RegisterEventBus()
    {
        var eventModuleType = typeof(EventBusModule<>).MakeGenericType(_dbContextType);
        var eventBusParameters = new EventBusModuleParameters(_services, _messageBrokerSettings, _moduleContext, _config, _compositionRoot);
        _eventBusModule = ((IEventBusModule) Activator.CreateInstance(eventModuleType, eventBusParameters)).Register();
        return this;
    }

    public IServicesModuleConfigurator RegisterMediator()
    {
        new MediatorModule(_services, _moduleContext).Register();
        return this;
    }

    public IAutoFacBuilderConfigurator ToAutofac()
    {
        var factory = new AutofacServiceProviderFactory();
        _builder = factory.CreateBuilder(_services);
        return this;
    }

    public IAutoFacBuilderConfiguratorWithLoggerFactory RegisterLoggerFactory()
    {
        _builder.RegisterModule(new LoggingModule(_moduleLogger));
        _loggerFactory = new SerilogLoggerFactory(_moduleLogger);

        _builder.RegisterInstance(_loggerFactory).As<ILoggerFactory>().SingleInstance();

        return this;
    }

    public IAutoFacBuilderConfiguratorWithDataAccess RegisterDataAccess()
    {
        var dataAccessModuleType = typeof(DataAccessModule<>).MakeGenericType(_dbContextType);
        var dataAccessModule = (IModule) Activator.CreateInstance(dataAccessModuleType, new DataAccessModuleParameters(_connectionString, _loggerFactory, _moduleContext))!;
        _builder.RegisterModule(dataAccessModule);

        return this;
    }


    public IAutoFacBuilderConfiguratorWithDataAccess RegisterInternalProcessing()
    {
        BiDictionary<string, Type> RegisterDomainEvents()
        {
            var domainRegistrations = new BiDictionary<string, Type>();
            return domainRegistrations;
        }

        var commandExectutor = new CommandsExecutor(new CompositionRoot(() => _container));
        _builder.RegisterInstance(commandExectutor).SingleInstance();

        _builder.RegisterModule(new ProcessingModule(_moduleContext));
        _builder.RegisterModule(new QuartzModule(_moduleContext));
        _builder.RegisterModule(new DomainEventsOutboxModule(RegisterDomainEvents(), _moduleContext));

        var outboxProcessor = new OutboxProcessor(_compositionRoot,
                                                  new SqlConnectionFactory(_connectionString),
                                                  new SimpleDomainNotificationMapper(),
                                                  _moduleContext.SchemaName);
        _quartz = new QuartzStartup(_moduleLogger, _internalProcessingPoolingInterval, _moduleContext.ModuleName, outboxProcessor);
        return this;
    }

    public IAutoFacBuilderConfigurator RegisterAssembliesServices()
    {
        _builder.RegisterAssemblyTypes([_moduleContext.InfrastructureAssembly, _moduleContext.ApplicationAssembly]).AsImplementedInterfaces().InstancePerLifetimeScope();
        return this;
    }

    public IAutoFacBuilderConfigurator RegisterHandlerDecorators()
    {
        _handlerDecorators = new HandlerDecorators(_moduleContext).RegisterDecorators(_builder);
        return this;
    }

    public IFinalizedConfig FinalizeConfig()
    {
        _builder.RegisterInstance(_moduleContext).SingleInstance();
        _config.RegisterServicesAction?.Apply(publicServiceRegistration => publicServiceRegistration(_builder));
        _container = _builder.Build();
        return this;
    }

    public static IServicesModuleConfigurator CreateConfigurator(string connectionString,
                                                                 MessageBrokerSettings messageBrokerSettings,
                                                                 ILogger logger,
                                                                 string schema,
                                                                 string moduleName,
                                                                 Assembly infrastructureAssembly,
                                                                 Assembly applicationAssembly,
                                                                 Assembly domainAssembly,
                                                                 bool useInternalProcessing = true,
                                                                 long? internalProcessingPoolingInterval = null,
                                                                 Action<IModuleStartupConfigurationBuilder>? config = null)
    {
        var moduleStartupConfig = new ModuleStartupConfiguration().Build(config);
        var moduleLogger = logger.ForContext("Module", moduleName);
        var moduleContext = new ModuleContext()
                            {
                                ModuleName             = moduleName,
                                SchemaName             = schema,
                                ApplicationAssembly    = applicationAssembly,
                                InfrastructureAssembly = infrastructureAssembly,
                                DomainAssembly         = domainAssembly
                            };
        var dbContextType = infrastructureAssembly.GetTypes().First(t => t.IsSubclassOf(typeof(BaseDbContext)));
        var services = new ServiceCollection();
        return new ModuleConfigurator(services, moduleStartupConfig, moduleLogger, moduleContext, dbContextType, messageBrokerSettings, connectionString, internalProcessingPoolingInterval);
    }

    public IContainer Container => _container;
    public IQuartz Quartz => _quartz;
    public IEventBusModule EventBusModule => _eventBusModule;
    public ILogger ModuleLogger => _moduleLogger;
    public ModuleContext ModuleContext => _moduleContext;
    public IModuleStartupConfiguration Config => _config;
}