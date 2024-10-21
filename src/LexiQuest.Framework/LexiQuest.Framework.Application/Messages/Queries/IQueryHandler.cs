using MassTransit;

namespace LexiQuest.Framework.Application.Messages.Queries;

public interface IQueryHandler<in TQuery, TResult> : IConsumer<TQuery>
    where TQuery : class, IQuery<TResult> where TResult : class;