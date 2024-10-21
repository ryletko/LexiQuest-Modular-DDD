using LexiQuest.Framework.Infrastructure.InternalProcessing.Outbox;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LexiQuest.Framework.Infrastructure.Dependencies;

public interface ICompositionRoot
{
    IScope BeginScope();
}

public interface IScope: IDisposable, IAsyncDisposable
{
    public IMediator GetMediator();
    public ILoggerFactory GetLoggerFactory();
    IBusControl GetBusControl();
    IOutboxErrorNotifier GetOutboxErrorNotifier();
    DbContext GetDbContext();
}