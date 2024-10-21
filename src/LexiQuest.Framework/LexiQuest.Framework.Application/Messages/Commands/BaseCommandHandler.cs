using MassTransit;

namespace LexiQuest.Framework.Application.Messages.Commands;

public abstract class BaseCommandHandler<T, R> : ICommandHandler<T, R> where T : class
                                                                       where R : class
{
    public async Task Consume(ConsumeContext<T> context)
    {
        var result = await Handle(context);
    }

    public abstract Task<R> Handle(ConsumeContext<T> context);
}