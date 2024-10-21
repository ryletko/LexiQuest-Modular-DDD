using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.Context;
using LexiQuest.Framework.Application.Messages.EventBus;
using MassTransit;

namespace LexiQuest.Framework.Infrastructure.EventBus;

// нужен только потому что из консюмера в медиаторе не отправляется сообщение в event bus через ipublishendpoint
// как вариант можно вообще отказаться от медиатора https://github.com/MassTransit/MassTransit/discussions/3607
// потому что потенциально так проще обрабатывать ошибки
public class MediatorEventBus(IBus bus,
                              IClientFactory clientFactory): IMediatorEventBus
{
    private readonly IEventBus _eventBus = new MassTransitBus(bus, clientFactory);

    public Task<Guid?> ExecCommand<T>(T command) where T : class, IContextedMessage 
        => _eventBus.ExecCommand(command);

    public Task<R> ExecCommand<T, R>(T command) where T : class, IContextedMessage where R : class
        => _eventBus.ExecCommand<T, R>(command);

    public Task<R> Query<T, R>(T command) where T : class, IContextedMessage where R : class
        => _eventBus.Query<T, R>(command);

    public Task SendEvent<T>(T command) where T : class, IContextedMessage
        => _eventBus.SendEvent(command);
}