using Quartz;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.InternalCommands;

[DisallowConcurrentExecution]
internal class ProcessInternalCommandsJob(CommandsExecutor commandsExecutor) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await commandsExecutor.Execute(new ProcessInternalCommandsCommand());
    }
}