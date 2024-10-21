using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.Framework.Infrastructure.EventBus;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.Outbox;

public class OutboxErrorNotifier(IEventBus eventBus): IOutboxErrorNotifier 
{
    public async Task NotifyError(Exception ex)
    {
        await eventBus.SendEvent(new ErrorNotification(ex.Message));
    }
}