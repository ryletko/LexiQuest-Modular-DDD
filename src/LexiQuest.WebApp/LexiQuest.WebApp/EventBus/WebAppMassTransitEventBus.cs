using System;
using System.Security.Claims;
using System.Threading.Tasks;
using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.Context;
using LexiQuest.Framework.Application.Messages.EventBus;
using MassTransit;
using Microsoft.AspNetCore.Http;
using MessageContext = LexiQuest.Framework.Application.Messages.Context.MessageContext;

namespace LexiQuest.WebApp.EventBus;

internal class WebAppMassTransitEventBus(IBus bus, // не могу понять почему publishEndpoing перестал работать
                                         IClientFactory clientFactory,
                                         IHttpContextAccessor httpContextAccessor) : IEventBus
{
    public async Task SendEvent<T>(T command) where T : class, IContextedMessage
    {
        command.MessageContext = new MessageContext(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, Guid.NewGuid());
        await bus.Publish(command);
    }

    public async Task<Guid?> ExecCommand<T>(T command) where T : class, IContextedMessage
    {
        command.MessageContext = new MessageContext(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, Guid.NewGuid());
        await bus.Publish(command);
        return command.MessageContext.CorrelationId;
    }

    public async Task<R> ExecCommand<T, R>(T command) where T : class, IContextedMessage
                                                      where R : class
    {
        command.MessageContext = new MessageContext(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, Guid.NewGuid());
        var requestClient = clientFactory.CreateRequestClient<T>();
        var response = await requestClient.GetResponse<R>(command);
        return response.Message;
    }


    public async Task<R> Query<T, R>(T command) where T : class, IContextedMessage
                                                where R : class
    {
        command.MessageContext = new MessageContext(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, Guid.NewGuid());
        var requestClient = clientFactory.CreateRequestClient<T>();
        var response = await requestClient.GetResponse<R>(command);
        return response.Message;
    }
}