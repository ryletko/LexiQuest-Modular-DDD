using LexiQuest.Framework.Application.Messages.Decoration;
using MassTransit;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.DomainEventsDispatching;

public class DomainEventsDispatcherNotificationHandlerDecorator<T>(IDomainEventsDispatcher domainEventsDispatcher) : IHandlerDecorator<T> where T : class
{
    public async Task Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T> iterator)
    {
        await iterator.Next();
        await domainEventsDispatcher.DispatchEventsAsync();
    }
}