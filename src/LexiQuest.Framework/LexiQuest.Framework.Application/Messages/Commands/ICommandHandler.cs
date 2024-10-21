using MassTransit;

namespace LexiQuest.Framework.Application.Messages.Commands;

public interface ICommandHandler<in TCommand> : IConsumer<TCommand>
    where TCommand : class;

public interface ICommandHandler<in TCommand, out TResult> : IConsumer<TCommand> where TCommand : class
                                                                                 where TResult : class;