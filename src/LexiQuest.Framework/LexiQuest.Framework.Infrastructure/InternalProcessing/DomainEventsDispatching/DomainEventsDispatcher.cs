using LexiQuest.Framework.Application.Messages.Events;
using LexiQuest.Framework.Application.Messages.InternalProcessing;
using LexiQuest.Framework.Domain;
using LexiQuest.Framework.Infrastructure.EventBus;
using LexiQuest.Framework.Infrastructure.Serialization;
using MassTransit.Mediator;
using Newtonsoft.Json;
using Utils.Core;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.DomainEventsDispatching;

public class DomainEventsDispatcher(IMediator mediator,
                                    IOutbox outbox,
                                    IDomainEventsAccessor domainEventsProvider,
                                    IDomainNotificationsMapper domainNotificationsMapper) : IDomainEventsDispatcher
{

    public async Task DispatchEventsAsync()
    {
        var domainEvents = domainEventsProvider.GetAllDomainEvents();
        //
        List<IMediatorDomainEvent<IDomainEvent>> domainEventNotifications = [];
        foreach (var domainEvent in domainEvents)
        {
            Type domainEvenNotificationType = typeof(MediatorDomainEvent<>);
            var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
            var domainNotification = (IMediatorDomainEvent<IDomainEvent>)Activator.CreateInstance(domainNotificationWithGenericType, domainEvent.Id, domainEvent);
            domainNotification.MessageContext = CommandContextAccessor.CurrentCommandContext?.Map(x => x with { });
            // var domainNotification = scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
            // {
            //     new NamedParameter("DomainEvent", domainEvent),
            //     new NamedParameter("Id", domainEvent.Id)
            // });
            //
            if (domainNotification != null)
            {
                domainEventNotifications.Add(domainNotification as IMediatorDomainEvent<IDomainEvent>);
            }
        }

        domainEventsProvider.ClearAllDomainEvents();

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }

        foreach (var domainEventNotification in domainEventNotifications.Where(x => x != null))
        {
            var type = domainNotificationsMapper.GetName(domainEventNotification.GetType());
            var data = JsonConvert.SerializeObject(domainEventNotification, new JsonSerializerSettings
                                                                            {   
                                                                                ContractResolver = new AllPropertiesContractResolver()
                                                                            });
        
            var outboxMessage = new OutboxMessage(domainEventNotification.Id,
                                                  domainEventNotification.DomainEvent.OccurredOn,
                                                  type,
                                                  data);
        
            outbox.Add(outboxMessage);
        }
    }
}