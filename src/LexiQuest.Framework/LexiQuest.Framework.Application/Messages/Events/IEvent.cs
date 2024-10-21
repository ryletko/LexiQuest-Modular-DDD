using LexiQuest.Framework.Application.Messages.Context;

namespace LexiQuest.Framework.Application.Messages.Events;

public interface IEvent: IContextedMessage
{
    Guid Id { get; }
}