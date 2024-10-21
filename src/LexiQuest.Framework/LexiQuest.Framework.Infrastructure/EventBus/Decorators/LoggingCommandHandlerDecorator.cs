using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Decoration;
using MassTransit;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace LexiQuest.Framework.Infrastructure.EventBus.Decorators;

public class LoggingCommandHandlerDecorator<T>(ILogger logger) : IHandlerDecorator<T> where T : class, ICommand
{
    public async Task Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T> iterator)
    {
        // if (context is IRecurringCommand)
        // {
            await iterator.Next();

        //     return;
        // }

        // using (LogContext.Push(new RequestLogEnricher(executionContextAccessor), new CommandLogEnricher(context.Message)))
        // {
        //     try
        //     {
        //         logger.Information("Executing command {Command}", context.GetType().Name);
        //         await iterator.Next();
        //         logger.Information("Command {Command} processed successful", context.GetType().Name);
        //     }
        //     catch (Exception exception)
        //     {
        //         logger.Error(exception, "Command {Command} processing failed", context.GetType().Name);
        //         throw;
        //     }
        // }
    }

    private class CommandLogEnricher(ICommand command) : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"Command:{command.Id.ToString()}")));
        }
    }

    // private class RequestLogEnricher(IExecutionContextAccessor executionContextAccessor) : ILogEventEnricher
    // {
    //     public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    //     {
    //         if (executionContextAccessor.IsAvailable)
    //         {
    //             logEvent.AddOrUpdateProperty(new LogEventProperty("CorrelationId", new ScalarValue(executionContextAccessor.CorrelationId)));
    //         }
    //     }
    // }
}

public class LoggingCommandHandlerDecorator<T, TR>(ILogger logger) : IHandlerDecorator<T, TR> where T : class, ICommand<TR>
                                                                                              where TR : class
{
    public async Task<TR> Consume(ConsumeContext<T> command, HandlerDecoratorIterator<T, TR> iterator)
    {
        // if (command is IRecurringCommand)
        // {
            return await iterator.Next();
        // }

        // using (LogContext.Push(new RequestLogEnricher(executionContextAccessor), new CommandLogEnricher(command.Message)))
        // {
        //     try
        //     {
        //         logger.Information("Executing command {@Command}", command);
        //         var result = await iterator.Next();
        //         logger.Information("Command processed successful, result {Result}", result);
        //         return result;
        //     }
        //     catch (Exception exception)
        //     {
        //         logger.Error(exception, "Command processing failed");
        //         throw;
        //     }
        // }
    }

    private class CommandLogEnricher(ICommand<TR> command) : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"Command:{command.Id.ToString()}")));
        }
    }

    // private class RequestLogEnricher(IExecutionContextAccessor executionContextAccessor) : ILogEventEnricher
    // {
    //     public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    //     {
    //         if (executionContextAccessor.IsAvailable)
    //         {
    //             logEvent.AddOrUpdateProperty(new LogEventProperty("CorrelationId", new ScalarValue(executionContextAccessor.CorrelationId)));
    //         }
    //     }
    // }
}