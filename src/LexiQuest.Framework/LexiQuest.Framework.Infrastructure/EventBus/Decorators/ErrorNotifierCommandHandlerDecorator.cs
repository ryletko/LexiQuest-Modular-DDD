using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Context;
using LexiQuest.Framework.Application.Messages.Decoration;
using LexiQuest.Framework.Application.Messages.Events;
using LexiQuest.Framework.Application.Messages.Queries;
using MassTransit;

namespace LexiQuest.Framework.Infrastructure.EventBus.Decorators;

// используется bus а не context.Publish потому что эксепшном контекст прерывается

internal static class ErrorNotificationFormer
{
    public static string FormErrorMessage<T>(T command, Exception ex)
    {
        return $"{command.GetType()}: {ex.Message}";
    }
}

public class ErrorNotifierCommandHandlerDecorator<T>(IBus bus) : IHandlerDecorator<T> where T : class, ICommand
{
    public async Task Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T> iterator)
    {
        try
        {
            await iterator.Next();
        }
        catch (Exception ex)
        {
            await bus.Publish(new ErrorNotification(ErrorNotificationFormer.FormErrorMessage(context.Message, ex)).ContextFrom(context.Message));
            throw;
        }
    }
}

public class ErrorNotifierCommandHandlerDecorator<T, TR>(IBus bus) : IHandlerDecorator<T, TR>
    where T : class, ICommand<TR>
    where TR : class
{
    public async Task<TR> Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T, TR> iterator)
    {
        try
        {
            return await iterator.Next();
        }
        catch (Exception ex)
        {
            await bus.Publish(new ErrorNotification(ErrorNotificationFormer.FormErrorMessage(context.Message, ex)).ContextFrom(context.Message));
            throw;
        }
    }
}

public class ErrorNotifierEventHandlerDecorator<T>(IBus bus) : IHandlerDecorator<T> where T : class, IEvent
{
    public async Task Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T> iterator)
    {
        try
        {
            await iterator.Next();
        }
        catch (Exception ex)
        {
            if (context.Message is not ErrorNotification)
            {
                await bus.Publish(new ErrorNotification(ErrorNotificationFormer.FormErrorMessage(context.Message, ex)).ContextFrom(context.Message));
            }

            throw;
        }
    }
}

public class ErrorNotifierQueryHandlerDecorator<T, R>(IBus bus) : IHandlerDecorator<T, R>
    where T : class, IQuery<R>
    where R : class
{
    public async Task<R> Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T, R> iterator)
    {
        try
        {
            return await iterator.Next();
        }
        catch (Exception ex)
        {
            await bus.Publish(new ErrorNotification(ErrorNotificationFormer.FormErrorMessage(context.Message, ex)).ContextFrom(context.Message));
            throw;
        }
    }
}