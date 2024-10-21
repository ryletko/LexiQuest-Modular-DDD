namespace LexiQuest.Framework.Application.DataAccess;

public interface IUnitOfWork
{
    Task<int> CommitAsync(
        CancellationToken cancellationToken = default,
        Guid? internalCommandId = null);
}