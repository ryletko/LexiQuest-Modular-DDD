using LexiQuest.Framework.Application.Messages.Commands;
using MassTransit;

namespace LexiQuest.Framework.Application.Messages.InternalProcessing.DomainEvents;

public interface IDomainEventHandler<in T>: IConsumer<T> where T: class, ICommand;