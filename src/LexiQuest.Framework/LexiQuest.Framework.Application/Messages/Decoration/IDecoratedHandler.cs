namespace LexiQuest.Framework.Application.Messages.Decoration;

public interface IDecoratedHandler<in T> where T : class
{
    Task Handle(T command, CancellationToken cancellationToken = default);
}

public interface IDecoratedHandler<in T, TR> where T : class
                                             where TR : class
{
    Task<TR> Handle(T query, CancellationToken cancellationToken = default);
}