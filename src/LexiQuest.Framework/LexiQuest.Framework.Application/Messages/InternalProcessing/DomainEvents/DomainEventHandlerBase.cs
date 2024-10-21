using LexiQuest.Framework.Application.Messages.Commands;
using MassTransit;

namespace LexiQuest.Framework.Application.Messages.InternalProcessing.DomainEvents;

// public abstract class DecoratedDomainEventHandler<T> : IDomainEventHandler<T>, IDecoratedHandler<T> where T : class, ICommand
// {
//     private static IEnumerable<IConsumerDecorator<T>> Decorators { get; set; }
//
//     public async Task Consume(ConsumeContext<T> context)
//     {
//         using (var iterator = new ConsumerHandlerDecoratorIterator<T>(context, this, Decorators))
//         {
//             await iterator.Next();
//         }
//     }
//
//     public abstract Task Handle(ConsumeContext<T> context);
// }

public abstract class DomainEventHandlerBase<T> : IDomainEventHandler<T> where T : class, ICommand
{
    public abstract Task Handle(T domainEvent);
    
    public async Task Consume(ConsumeContext<T> context)
    {
        await Handle(context.Message);
    }
}