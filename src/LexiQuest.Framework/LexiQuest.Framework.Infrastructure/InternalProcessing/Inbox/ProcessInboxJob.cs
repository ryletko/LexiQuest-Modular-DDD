using Quartz;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.Inbox;

[DisallowConcurrentExecution]
internal class ProcessInboxJob(CommandsExecutor commandsExecutor) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await commandsExecutor.Execute(new ProcessInboxCommand());
    }
}