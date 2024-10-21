using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.Context;
using LexiQuest.Framework.Application.Messages.EventBus;
using MassTransit;
using Utils.Core;
using MessageContext = LexiQuest.Framework.Application.Messages.Context.MessageContext;

namespace LexiQuest.Framework.Infrastructure.EventBus;

public class MassTransitBus(IPublishEndpoint bus,
        //IBus bus,                    
        IClientFactory clientFactory) : IEventBus
{
    private MessageContext? CopyContext() => CommandContextAccessor.CurrentCommandContext?.Map(c => c with { });

    public async Task SendEvent<T>(T command) where T : class, IContextedMessage
    {
        command.MessageContext = CopyContext();
        await bus.Publish(command);
    }

    public async Task<Guid?> ExecCommand<T>(T command) where T : class, IContextedMessage
    {
        command.MessageContext = CopyContext();
        await bus.Publish(command);
        return command.MessageContext?.CorrelationId;
    }

    public async Task<R> ExecCommand<T, R>(T command) where T : class, IContextedMessage
                                                      where R : class
    {
        command.MessageContext = CopyContext();
        var requestClient = clientFactory.CreateRequestClient<T>();
        var response = await requestClient.GetResponse<R>(command);
        return response.Message;
    }

    public async Task<R> Query<T, R>(T command) where T : class, IContextedMessage
                                                where R : class
    {
        command.MessageContext = CopyContext();
        var requestClient = clientFactory.CreateRequestClient<T>();
        var response = await requestClient.GetResponse<R>(command);
        return response.Message;
    }
}