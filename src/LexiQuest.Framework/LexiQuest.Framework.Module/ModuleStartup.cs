using System.Reflection;
using Autofac;
using LexiQuest.Framework.Infrastructure.EventBus;
using LexiQuest.Framework.Module.Config;
using LexiQuest.Framework.Module.DataAccess;
using LexiQuest.Framework.Module.EventBus;
using LexiQuest.Framework.Module.InternalProcessing;
using LexiQuest.Framework.Module.Quartz;
using Utils.Core;
using ILogger = Serilog.ILogger;

namespace LexiQuest.Framework.Module;

public class ModuleStartup
{
    const bool RunInternalProcess = true;

    private ModuleStartup(IContainer container, ModuleContext moduleContext, IModuleStartupConfiguration config, ILogger moduleLogger, IEventBusModule eventBusModule, IQuartz quartz)
    {
        _container      = container;
        _moduleContext  = moduleContext;
        _config         = config;
        _moduleLogger   = moduleLogger;
        _eventBusModule = eventBusModule;
        _quartz         = quartz;
    }

    private readonly IContainer _container;
    private readonly IQuartz _quartz;
    private readonly IEventBusModule _eventBusModule;
    private readonly ILogger _moduleLogger;
    private readonly ModuleContext _moduleContext;
    private readonly IModuleStartupConfiguration _config;
    
    public static ModuleStartup Configure(string connectionString,
                                          MessageBrokerSettings messageBrokerSettings,
                                          ILogger logger,
                                          string schema,
                                          string moduleName,
                                          Assembly infrastructureAssembly,
                                          Assembly applicationAssembly,
                                          Assembly domainAssembly,
                                          bool useInternalProcessing = true,
                                          long? internalProcessingPoolingInterval = null,
                                          Action<IModuleStartupConfigurationBuilder>? config = null) =>
        ModuleConfigurator.CreateConfigurator(connectionString,
                                              messageBrokerSettings,
                                              logger,
                                              schema,
                                              moduleName,
                                              infrastructureAssembly,
                                              applicationAssembly,
                                              domainAssembly,
                                              useInternalProcessing,
                                              internalProcessingPoolingInterval,
                                              config)
                          .RegisterHttpClient()
                          .RegisterEventBus()
                          .RegisterMediator()
                          .ToAutofac()
                          .RegisterLoggerFactory()
                          .RegisterDataAccess()
                          .RegisterInternalProcessing()
                          .RegisterHandlerDecorators()
                          .RegisterAssembliesServices()
                          .FinalizeConfig()
                          .Map(c => new ModuleStartup(c.Container,
                                                      c.ModuleContext,
                                                      c.Config,
                                                      c.ModuleLogger,
                                                      c.EventBusModule,
                                                      c.Quartz));
    
    public async Task<ModuleStartup> Start()
    {
        Migrator.ApplyDbMigrations(new CompositionRoot(() => _container));
        await _eventBusModule.Start();
        if (RunInternalProcess)
            await _quartz.Start();

        _moduleLogger.Information("Module started");

        return this;
    }
    
    public async Task Stop()
    {
        await _quartz.Stop();
        await _eventBusModule.Stop();
        _moduleLogger.Information("Module stopped");
    }
}