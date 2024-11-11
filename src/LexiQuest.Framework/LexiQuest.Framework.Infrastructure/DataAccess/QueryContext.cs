using LexiQuest.Framework.Application.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.Framework.Infrastructure.DataAccess;

public class QueryContext(BaseDbContext baseDbContext): IQueryContext 
{
    public IQueryable<TEntity> Query<TEntity>() where TEntity : class
    {
        return baseDbContext.Set<TEntity>().AsQueryable().AsNoTracking();
    }
}