using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LexiQuest.Framework.Application;
using LexiQuest.Framework.Infrastructure.EventBus;
using LexiQuest.Import.GoogleSheets;
using LexiQuest.Module;
using LexiQuest.PuzzleMgr.Infrastructure.Config;
using LexiQuest.QuizGame.Infrastructure.Config;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace LexiQuest.WebApp;

public class ModulesHostedService(ConnectionString connectionString,
                                  MessageBrokerSettings messageBrokerSettings,
                                  ILogger logger) : IHostedService
{
    private List<ILexiQuestModule> ModuleStartups;

    private async Task StartModules()
    {
        foreach (var moduleStartup in ModuleStartups)
        {
            await moduleStartup.Start();
        }
    }

    private void ConfigureModules()
    {
        foreach (var moduleStartup in ModuleStartups)
        {
            moduleStartup.Configure();
        }
    }

    public ModulesHostedService Configure()
    {
        ModuleStartups =
        [
            new PuzzleMgrModuleStartup(connectionString.Value, messageBrokerSettings, logger),
            new QuizGameModuleStartup(connectionString.Value, messageBrokerSettings, logger),
            new GoogleImportModuleStartup(connectionString.Value, messageBrokerSettings, logger)
        ];

        ConfigureModules();
        return this;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await StartModules();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var moduleStartup in ModuleStartups)
        {
            await moduleStartup.Stop();
        }
    }
}