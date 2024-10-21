using LexiQuest.Framework.Infrastructure.EventBus;
using LexiQuest.Framework.Module;
using LexiQuest.Module;
using Serilog;

namespace LexiQuest.QuizGame.Infrastructure.Config;

public class QuizGameModuleStartup(string connectionString,
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
                                          "QuizGame",
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