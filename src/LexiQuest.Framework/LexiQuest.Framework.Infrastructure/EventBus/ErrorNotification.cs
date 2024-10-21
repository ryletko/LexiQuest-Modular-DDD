using LexiQuest.Framework.Application.Messages.Events;

namespace LexiQuest.Framework.Infrastructure.EventBus;

public record ErrorNotification(string ErrorMessage): EventBase;