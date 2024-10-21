using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Context;

namespace LexiQuest.Framework.Application.Messages.InternalProcessing;

public abstract class InternalCommandBase(Guid id) : ICommand
{
    public MessageContext? MessageContext { get; set; }

    public Guid Id { get; } = id;
}

public abstract class InternalCommandBase<TResult>(Guid id) : ICommand<TResult>
    where TResult : class
{
    
    public MessageContext? MessageContext { get; set; }

    protected InternalCommandBase() : this(Guid.NewGuid())
    {
    }
    
    public Guid Id { get; } = id;
}