using MassTransit;

namespace LexiQuest.Framework.Application.Messages.Events;

public interface IEventHandler<in TEvent> : IConsumer<TEvent> where TEvent : class, IEvent;