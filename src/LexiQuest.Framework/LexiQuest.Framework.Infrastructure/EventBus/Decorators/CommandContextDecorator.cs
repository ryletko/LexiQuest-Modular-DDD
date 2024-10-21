using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Context;
using LexiQuest.Framework.Application.Messages.Decoration;
using LexiQuest.Framework.Application.Messages.Events;
using LexiQuest.Framework.Application.Messages.Queries;
using MassTransit;
using Utils.Core;

namespace LexiQuest.Framework.Infrastructure.EventBus.Decorators;

public class CommandContextDecorator<T> : IHandlerDecorator<T> where T : class, ICommand
{
    public async Task Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T> iterator)
    {
        CommandContextAccessor.CurrentCommandContext = context.Message.MessageContext?.Map(c => c with { });
        await iterator.Next();
    }
}

public class CommandContextDecorator<T, TR> : IHandlerDecorator<T, TR>
    where T : class, ICommand<TR>
    where TR : class, IContextedMessage
{
    public async Task<TR> Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T, TR> iterator)
    {
        CommandContextAccessor.CurrentCommandContext = context.Message.MessageContext?.Map(c => c with { });
        var result = await iterator.Next();
        result.MessageContext = context.Message.MessageContext?.Map(c => c with { });
        return result;
    }
}

public class QueryContextDecorator<T, R> : IHandlerDecorator<T, R>
    where T : class, IQuery<R>
    where R : class
{
    public async Task<R> Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T, R> iterator)
    {
        CommandContextAccessor.CurrentCommandContext = context.Message.MessageContext?.Map(c => c with { });
        var result = await iterator.Next();
        // result.MessageContext = context.Message.MessageContext?.Map(c => c with { });
        return result;
    }
}

public class EventContextDecorator<T> : IHandlerDecorator<T> where T : class, IEvent
{
    public async Task Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T> iterator)
    {
        CommandContextAccessor.CurrentCommandContext = context.Message.MessageContext?.Map(c => c with { });
        await iterator.Next();
    }
}