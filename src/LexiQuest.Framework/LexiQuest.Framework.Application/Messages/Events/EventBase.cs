using LexiQuest.Framework.Application.Messages.Context;

namespace LexiQuest.Framework.Application.Messages.Events;

public abstract record EventBase(Guid Id): IEvent
{
    public MessageContext? MessageContext { get; set; }
    
    protected EventBase() : this(Guid.NewGuid())
    {
    }

}