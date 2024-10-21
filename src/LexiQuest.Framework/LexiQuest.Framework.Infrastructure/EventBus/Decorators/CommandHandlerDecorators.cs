using System.Collections;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Context;
using LexiQuest.Framework.Application.Messages.Decoration;
using LexiQuest.Framework.Application.Messages.Events;
using LexiQuest.Framework.Application.Messages.Queries;

namespace LexiQuest.Framework.Infrastructure.EventBus.Decorators;

public class CommandHandlerDecorators<T>(CommandContextDecorator<T> commandContextDecorator,
                                         UnitOfWorkCommandHandlerDecorator<T> unitOfWorkCommandHandlerDecorator,
                                         ValidationCommandHandlerDecorator<T> validationCommandHandlerDecorator,
                                         LoggingCommandHandlerDecorator<T> loggingCommandHandlerDecorator,
                                         ErrorNotifierCommandHandlerDecorator<T> errorNotifierCommandHandlerDecorator) : ICommandHandlerDecorators<T>
    where T : class, ICommand
{
    public IEnumerator<IHandlerDecorator<T>> GetEnumerator()
    {
        yield return commandContextDecorator;
        yield return errorNotifierCommandHandlerDecorator;
        yield return unitOfWorkCommandHandlerDecorator;
        yield return validationCommandHandlerDecorator;
        // yield return loggingCommandHandlerDecorator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class CommandHandlerDecorators<T, R>(CommandContextDecorator<T, R> commandContextDecorator,
                                            UnitOfWorkCommandHandlerDecorator<T, R> unitOfWorkCommandHandlerDecorator,
                                            ValidationCommandHandlerDecorator<T, R> validationCommandHandlerDecorator,
                                            LoggingCommandHandlerDecorator<T, R> loggingCommandHandlerDecorator,
                                            ErrorNotifierCommandHandlerDecorator<T, R> errorNotifierCommandHandlerDecorator) : ICommandHandlerDecorators<T, R>
    where T : class, ICommand<R>
    where R : class, IContextedMessage
{
    public IEnumerator<IHandlerDecorator<T, R>> GetEnumerator()
    {
        yield return commandContextDecorator;
        yield return errorNotifierCommandHandlerDecorator;
        yield return unitOfWorkCommandHandlerDecorator;
        yield return validationCommandHandlerDecorator;
        // yield return loggingCommandHandlerDecorator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class QueryHandlerDecorators<T, R>(QueryContextDecorator<T, R> commandContextDecorator,
                                          ErrorNotifierQueryHandlerDecorator<T, R> errorNotifierQueryHandlerDecorator) : IQueryHandlerDecorators<T, R>
    where T : class, IQuery<R>
    where R : class
{
    public IEnumerator<IHandlerDecorator<T, R>> GetEnumerator()
    {
        yield return commandContextDecorator;
        yield return errorNotifierQueryHandlerDecorator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class EventHandlerDecorators<T>(EventContextDecorator<T> commandContextDecorator,
                                       UnitOfWorkEventHandlerDecorator<T> unitOfWorkCommandHandlerDecorator,
                                       ErrorNotifierEventHandlerDecorator<T> errorNotifierEventHandlerDecorator) : IEventHandlerDecorators<T>
    where T : class, IEvent
{
    public IEnumerator<IHandlerDecorator<T>> GetEnumerator()
    {
        yield return commandContextDecorator;
        yield return errorNotifierEventHandlerDecorator;
        yield return unitOfWorkCommandHandlerDecorator;
        // yield return loggingCommandHandlerDecorator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}