namespace LexiQuest.Framework.Infrastructure.InternalProcessing.DomainEventsDispatching;

public interface IDomainNotificationsMapper
{
    string GetName(Type type);

    Type GetType(string name);
}