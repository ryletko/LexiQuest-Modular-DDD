namespace LexiQuest.Framework.Application.Messages.InternalProcessing
{
    public interface IOutbox
    {
        void Add(OutboxMessage message);

        Task Save();
    }
}