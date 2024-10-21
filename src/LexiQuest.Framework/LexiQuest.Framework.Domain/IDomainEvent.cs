namespace LexiQuest.Framework.Domain;

public interface IDomainEvent
{
    Guid Id { get; }

    DateTime OccurredOn { get; }
}