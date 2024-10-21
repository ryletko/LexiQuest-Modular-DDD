using LexiQuest.Framework.Application.DataAccess;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Decoration;
using LexiQuest.Framework.Application.Messages.Events;
using LexiQuest.Framework.Infrastructure.DataAccess;
using MassTransit;

namespace LexiQuest.Framework.Infrastructure.EventBus.Decorators;

public class UnitOfWorkCommandHandlerDecorator<T>(IUnitOfWork unitOfWork,
                                                  BaseDbContext dbContext) : IHandlerDecorator<T> where T : class, ICommand
{
    public async Task Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T> iterator)
    {
        await iterator.Next();

        // if (context is InternalCommandBase)
        // {
        //     var internalCommand = await dbContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == context.Message.Id, cancellationToken: context.CancellationToken);
        //
        //     if (internalCommand != null)
        //     {
        //         internalCommand.ProcessedDate = DateTime.UtcNow;
        //     }
        // }

        await unitOfWork.CommitAsync(context.CancellationToken);
    }
}

public class UnitOfWorkCommandHandlerDecorator<T, TR>(IUnitOfWork unitOfWork,
                                                      BaseDbContext dbContext) : IHandlerDecorator<T, TR>
    where T : class, ICommand<TR>
    where TR : class
{
    public async Task<TR> Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T, TR> iterator)
    {
        var result = await iterator.Next();

        // if (context is InternalCommandBase<TR>)
        // {
        //     var internalCommand = await dbContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == context.Message.Id, cancellationToken: context.CancellationToken);
        //
        //     if (internalCommand != null)
        //     {
        //         internalCommand.ProcessedDate = DateTime.UtcNow;
        //     }
        // }

        await unitOfWork.CommitAsync(context.CancellationToken);

        return result;
    }
}

public class UnitOfWorkEventHandlerDecorator<T>(IUnitOfWork unitOfWork,
                                                BaseDbContext dbContext) : IHandlerDecorator<T> where T : class, IEvent
{
    public async Task Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T> iterator)
    {
        await iterator.Next();

        // if (context is InternalCommandBase)
        // {
        //     var internalCommand = await dbContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == context.Message.Id, cancellationToken: context.CancellationToken);
        //
        //     if (internalCommand != null)
        //     {
        //         internalCommand.ProcessedDate = DateTime.UtcNow;
        //     }
        // }

        await unitOfWork.CommitAsync(context.CancellationToken);
    }
}