using LexiQuest.Framework.Domain;

namespace LexiQuest.Framework.Application.Messages.Events;

public interface IMediatorDomainEvent: IEvent;

public interface IMediatorDomainEvent<out T> : IMediatorDomainEvent where T : IDomainEvent
{
    public T DomainEvent { get; }
}

public record MediatorDomainEvent<T>(Guid Id, T DomainEvent): EventBase(Id), IMediatorDomainEvent<T> where T : IDomainEvent
{

}