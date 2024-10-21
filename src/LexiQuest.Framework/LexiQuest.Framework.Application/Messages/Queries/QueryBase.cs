using LexiQuest.Framework.Application.Messages.Context;

namespace LexiQuest.Framework.Application.Messages.Queries;

public abstract record QueryBase<TResult>(Guid Id) : IQuery<TResult>
    where TResult : class
{
    public MessageContext? MessageContext { get; set; }
    
    protected QueryBase() : this(Guid.NewGuid())
    {
    }
}