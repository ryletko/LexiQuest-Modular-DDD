using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Events;
using LexiQuest.Framework.Application.Messages.Queries;

namespace LexiQuest.Framework.Application.Messages.Decoration;

public interface ICommandHandlerDecorators<T> : IEnumerable<IHandlerDecorator<T>> where T : class, ICommand;
public interface ICommandHandlerDecorators<T, TR> : IEnumerable<IHandlerDecorator<T, TR>> where T : class, ICommand<TR> where TR : class;
public interface IQueryHandlerDecorators<T, TR> : IEnumerable<IHandlerDecorator<T, TR>> where T : class, IQuery<TR> where TR : class;
public interface IEventHandlerDecorators<T> : IEnumerable<IHandlerDecorator<T>> where T : class, IEvent;
