namespace LexiQuest.Framework.Infrastructure.InternalProcessing.Outbox;

public interface IOutboxErrorNotifier
{
    public Task NotifyError(Exception ex);
}