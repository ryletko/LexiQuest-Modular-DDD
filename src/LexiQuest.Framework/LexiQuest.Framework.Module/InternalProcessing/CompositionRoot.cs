using Autofac;
using LexiQuest.Framework.Infrastructure.Dependencies;
using LexiQuest.Framework.Infrastructure.InternalProcessing.Outbox;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LexiQuest.Framework.Module.InternalProcessing;

public class CompositionRoot(Func<IContainer> container): ICompositionRoot
{
    public IScope BeginScope()
    {
        return new Scope(container().BeginLifetimeScope());
    }
}

public class Scope(ILifetimeScope autofacScope): IScope
{
    
    public IMediator GetMediator()
    {
        return autofacScope.Resolve<IMediator>();
    }

    public ILoggerFactory GetLoggerFactory()
    {
        return autofacScope.Resolve<ILoggerFactory>();
    }

    public IBusControl GetBusControl()
    {
        return autofacScope.Resolve<IBusControl>();
    }

    public IOutboxErrorNotifier GetOutboxErrorNotifier()
    {
        return autofacScope.Resolve<IOutboxErrorNotifier>();
    }

    public DbContext GetDbContext()
    {
        return autofacScope.Resolve<DbContext>();
    }

    public async ValueTask DisposeAsync()
    {
        await autofacScope.DisposeAsync();
    }

    public void Dispose()
    {
        autofacScope.Dispose();
    }
}