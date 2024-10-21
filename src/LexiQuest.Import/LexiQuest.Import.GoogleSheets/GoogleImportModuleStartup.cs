using Autofac;
using LexiQuest.Framework.Infrastructure.EventBus;
using LexiQuest.Framework.Module;
using LexiQuest.Import.GoogleSheets.Config;
using LexiQuest.Import.GoogleSheets.Http;
using LexiQuest.Import.GoogleSheets.Services;
using LexiQuest.Module;
using Serilog;

namespace LexiQuest.Import.GoogleSheets;

public class GoogleImportModuleStartup(string connectionString,
                                        MessageBrokerSettings messageBrokerSettings,
                                        ILogger logger) : ILexiQuestModule
{
    private ModuleStartup _module;

    private const string Schema = GoogleImportDbContext.SchemaNameStatic;

    public ILexiQuestModule Configure()
    {
        _module = ModuleStartup.Configure(connectionString,
                                          messageBrokerSettings,
                                          logger,
                                          Schema,
                                          "GoogleImportModule",
                                          Assemblies.GoogleImportAssembly,
                                          Assemblies.GoogleImportAssembly,
                                          Assemblies.GoogleImportAssembly,
                                          useInternalProcessing: false,
                                          config: x =>
                                                  {
                                                      x.OverwriteEventBusScanFilter();
                                                      x.RegisterServices(builder =>
                                                                         {
                                                                             builder.RegisterType<GoogleXlsxImportService>().AsSelf().InstancePerLifetimeScope();
                                                                             builder.RegisterType<GoogleExcelProvider>().As<IGoogleExcelProvider>().InstancePerLifetimeScope();
                                                                         });
                                                  });
        return this;
    }

    public async Task<ILexiQuestModule> Start()
    {
        await _module.Start();
        return this;
    }

    public async Task Stop()
    {
        await _module.Stop();
    }
}