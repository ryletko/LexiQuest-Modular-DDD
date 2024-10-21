using LexiQuest.Framework.Application;
using LexiQuest.Framework.Application.DataAccess;
using LexiQuest.Framework.Infrastructure.InternalProcessing.DomainEventsDispatching;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.Framework.Infrastructure.DataAccess;

public class UnitOfWork(DbContext context, IDomainEventsDispatcher domainEventsDispatcher) : IUnitOfWork
{
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default,
                                       Guid? internalCommandId = null)
    {
        await domainEventsDispatcher.DispatchEventsAsync();
        return await context.SaveChangesAsync(cancellationToken);
    }
}