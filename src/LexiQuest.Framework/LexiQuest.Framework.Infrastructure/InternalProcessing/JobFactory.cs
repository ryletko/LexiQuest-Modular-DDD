using LexiQuest.Framework.Infrastructure.InternalProcessing.Outbox;
using Quartz;
using Quartz.Spi;

namespace LexiQuest.Framework.Module.Quartz;

internal class JobFactory(OutboxProcessor outboxProcessor) : IJobFactory
{
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return (IJob) Activator.CreateInstance(bundle.JobDetail.JobType, outboxProcessor);
    }

    public void ReturnJob(IJob job)
    {
    }
}