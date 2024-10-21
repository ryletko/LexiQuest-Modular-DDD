using LexiQuest.Framework.Application;
using LexiQuest.Framework.Application.DataAccess;
using LexiQuest.Framework.Infrastructure.DataAccess;
using MassTransit;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.DomainEventsDispatching;

public class UnitOfWorkCommandHandlerDecorator<T>(IConsumer<T> decorated,
                                                  IUnitOfWork unitOfWork) : IConsumer<T> where T : class
{
    public async Task Consume(ConsumeContext<T> context)
    {
        await decorated.Consume(context);
        await unitOfWork.CommitAsync(context.CancellationToken);
    }
}