using LexiQuest.Framework.Application.Messages.Context;

namespace LexiQuest.Framework.Application.Messages.Commands;

public abstract record CommandBase(Guid Id) : ICommand
{
    public MessageContext? MessageContext { get; set; }

    protected CommandBase() : this(Guid.NewGuid())
    {
    }
}

public abstract record CommandBase<TResult>(Guid Id) : ICommand<TResult>
    where TResult : class
{
    public MessageContext? MessageContext { get; set; }

    protected CommandBase() : this(Guid.NewGuid())
    {
    }
}

