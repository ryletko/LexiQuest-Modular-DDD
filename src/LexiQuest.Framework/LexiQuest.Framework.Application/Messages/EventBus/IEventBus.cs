using LexiQuest.Framework.Application.Messages.Context;

namespace LexiQuest.Framework.Application.Messages.EventBus;

public interface IEventBus
{
    Task<Guid?> ExecCommand<T>(T command) where T : class, IContextedMessage;

    Task<R> ExecCommand<T, R>(T command) where T : class, IContextedMessage
                                         where R : class;

    Task<R> Query<T, R>(T command) where T : class, IContextedMessage
                                   where R : class;

    Task SendEvent<T>(T command) where T : class, IContextedMessage;
}