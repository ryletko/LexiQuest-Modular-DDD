using LexiQuest.Framework.Application.Messages.Context;
using LexiQuest.Framework.Application.Messages.Decoration;
using MassTransit;

namespace LexiQuest.Framework.Application.Messages.Commands;
 
public abstract class CommandHandlerBase<T> : ICommandHandler<T>, IDecoratedHandler<T> where T : class, ICommand
{
    private ICommandHandlerDecorators<T> _decorators;

    public async Task Consume(ConsumeContext<T> context)
    {
        using (var iterator = new HandlerDecoratorIterator<T>(context, this, _decorators))
        {
            await iterator.Next();
        }
    }

    public abstract Task Handle(T command, CancellationToken cancellationToken);

}

public abstract class CommandHandlerBase<T, TR> : ICommandHandler<T, TR>, IDecoratedHandler<T, TR>
    where T : class, ICommand<TR>
    where TR : class, IContextedMessage
{
    private ICommandHandlerDecorators<T, TR> _decorators;

    public async Task Consume(ConsumeContext<T> context)
    {
        TR result;
        using (var iterator = new HandlerDecoratorIterator<T, TR>(context, this, _decorators))
        {
            result = await iterator.Next();
        }

        await context.RespondAsync(result);
    }

    public abstract Task<TR> Handle(T query, CancellationToken cancellationToken = default);
}