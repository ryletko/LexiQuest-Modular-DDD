using LexiQuest.Framework.Application.Messages.Decoration;
using MassTransit;

namespace LexiQuest.Framework.Application.Messages.Events;

public abstract class EventHandlerBase<T> : IEventHandler<T>, IDecoratedHandler<T>
    where T : class, IEvent
{
    private IEventHandlerDecorators<T> _decorators;

    public async Task Consume(ConsumeContext<T> context)
    {
        using (var iterator = new HandlerDecoratorIterator<T>(context, this, _decorators))
        {
            await iterator.Next();
        }
    }

    public abstract Task Handle(T command, CancellationToken cancellationToken);
}

