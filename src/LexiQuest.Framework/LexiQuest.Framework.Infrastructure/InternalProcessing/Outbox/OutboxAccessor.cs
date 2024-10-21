using LexiQuest.Framework.Application.Messages.InternalProcessing;
using LexiQuest.Framework.Infrastructure.DataAccess;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.Outbox;

public class OutboxAccessor : IOutbox
{
    private readonly BaseDbContext _dbContext;

    internal OutboxAccessor(BaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(OutboxMessage message)
    {
        _dbContext.OutboxMessages.Add(message);
    }

    public Task Save()
    {
        return Task.CompletedTask; // Save is done automatically using EF Core Change Tracking mechanism during SaveChanges.
    }
}