using MassTransit;

namespace LexiQuest.Framework.Application.Messages.Decoration;

public class HandlerDecoratorIterator<T>(ConsumeContext<T> context,
                                                 IDecoratedHandler<T> decorated,
                                                 IEnumerable<IHandlerDecorator<T>> decorators) : IDisposable
    where T : class
{
    private readonly IEnumerator<IHandlerDecorator<T>> _enumerator = decorators.GetEnumerator();
    private bool _disposed = false;

    public async Task Next()
    {
        if (_enumerator.MoveNext())
        {
            await _enumerator.Current.Consume(context, this);
        }
        else
        {
            await decorated.Handle(context.Message);
        }
    }
    
    public void Dispose()
    {
        if (_disposed)
            return;
        
        _disposed = true;

        _enumerator.Dispose();
    }
}
