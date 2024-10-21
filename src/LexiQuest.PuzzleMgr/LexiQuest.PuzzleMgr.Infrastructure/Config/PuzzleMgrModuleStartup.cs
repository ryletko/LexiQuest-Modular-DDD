using LexiQuest.Framework.Infrastructure.EventBus;
using LexiQuest.Framework.Module;
using LexiQuest.Module;
using LexiQuest.PuzzleMgr.Infrastructure.Config.DataAccess;
using Serilog;

namespace LexiQuest.PuzzleMgr.Infrastructure.Config;

public class PuzzleMgrModuleStartup(string connectionString,
                                    MessageBrokerSettings messageBrokerSettings,
                                    ILogger logger) : ILexiQuestModule
{
    private ModuleStartup _module { get; set; }

    public ILexiQuestModule Configure()
    {
        _module = ModuleStartup.Configure(connectionString,
                                          messageBrokerSettings,
                                          logger,
                                          Schema.Name,
                                          "PuzzleMgr",
                                          Assemblies.Infrastructure,
                                          Assemblies.Application,
                                          Assemblies.Domain);

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