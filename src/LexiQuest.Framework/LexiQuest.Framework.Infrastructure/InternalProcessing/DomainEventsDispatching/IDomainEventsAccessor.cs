using LexiQuest.Framework.Domain;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.DomainEventsDispatching;

public interface IDomainEventsAccessor
{
    IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

    void ClearAllDomainEvents();
}