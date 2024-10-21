using MassTransit;

namespace LexiQuest.Framework.Application.Messages.Decoration;

public interface IHandlerDecorator<T> where T : class
{
    Task Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T> iterator);
}

public interface IHandlerDecorator<T, TR> where T : class 
                                           where TR: class
{
    Task<TR> Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T, TR> iterator);
}