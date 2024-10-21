using System.Collections.Specialized;
using LexiQuest.Framework.Infrastructure.InternalProcessing.Outbox;
using LexiQuest.Framework.Module.Quartz;
using Quartz;
using Quartz.Impl;
using Serilog;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.Quartz;

public class QuartzStartup(ILogger logger,
                           long? internalProcessingPoolingInterval,
                           string name,
                           OutboxProcessor outboxProcessor) : IQuartz
{
    private IScheduler _scheduler;

    // TODO convert to async
    public async Task<IQuartz> Start()
    {
        logger.Information("Quartz starting...");

        var schedulerConfiguration = new NameValueCollection();
        schedulerConfiguration.Add("quartz.scheduler.instanceName", name);

        ISchedulerFactory schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
        _scheduler            = await schedulerFactory.GetScheduler();
        _scheduler.JobFactory = new JobFactory(outboxProcessor);

        // LogProvider.SetCurrentLogProvider(new SerilogLogProvider(logger));

        await _scheduler.Start();

        var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();

        ITrigger trigger;
        if (internalProcessingPoolingInterval.HasValue)
        {
            trigger =
                TriggerBuilder
                   .Create()
                   .StartNow()
                   .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromMilliseconds(internalProcessingPoolingInterval.Value)).RepeatForever())
                   .Build();
        }
        else
        {
            trigger =
                TriggerBuilder
                   .Create()
                   .StartNow()
                   .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromMilliseconds(100)).RepeatForever())
                    // .WithCronSchedule("0/2 * * ? * *")
                   .Build();
        }

        _scheduler.ScheduleJob(processOutboxJob, trigger).GetAwaiter().GetResult();
        //
        // var processInboxJob = JobBuilder.Create<ProcessInboxJob>().Build();
        // var processInboxTrigger =
        //     TriggerBuilder
        //        .Create()
        //        .StartNow()
        //        .WithCronSchedule("0/2 * * ? * *")
        //        .Build();
        //
        // _scheduler
        //    .ScheduleJob(processInboxJob, processInboxTrigger)
        //    .GetAwaiter().GetResult();
        //
        // var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
        // var triggerCommandsProcessing =
        //     TriggerBuilder
        //        .Create()
        //        .StartNow()
        //        .WithCronSchedule("0/2 * * ? * *")
        //        .Build();
        // _scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();

        logger.Information("Quartz started.");
        return this;
    }

    public async Task Stop()
    {
        await _scheduler.Shutdown();
    }
}