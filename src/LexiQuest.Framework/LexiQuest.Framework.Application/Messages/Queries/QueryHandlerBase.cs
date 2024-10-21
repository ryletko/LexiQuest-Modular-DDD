using LexiQuest.Framework.Application.Messages.Decoration;
using MassTransit;

namespace LexiQuest.Framework.Application.Messages.Queries;

public abstract class QueryHandlerBase<T, TR> : IQueryHandler<T, TR>, IDecoratedHandler<T, TR>
    where T : class, IQuery<TR>
    where TR : class
{
    private IQueryHandlerDecorators<T, TR> _decorators;

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