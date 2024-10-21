namespace LexiQuest.Framework.Application.DataAccess;

public interface IQueryContext
{
    IQueryable<TEntity> Query<TEntity>() where TEntity : class;
}