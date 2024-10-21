using LexiQuest.Framework.Application.Messages.Context;
using MassTransit.Mediator;

namespace LexiQuest.Framework.Application.Messages.Commands;

public interface ICommand<out TResult> : IContextedMessage, Request<TResult> where TResult : class
{
    Guid Id { get; }
}

public interface ICommand : IContextedMessage
{
    Guid Id { get; }
}