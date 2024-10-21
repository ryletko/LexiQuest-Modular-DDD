namespace LexiQuest.Framework.Infrastructure.InternalProcessing.DomainEventsDispatching
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}