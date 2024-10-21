using System.Threading.Tasks;
using LexiQuest.Framework.Infrastructure.EventBus;
using LexiQuest.WebApp.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace LexiQuest.WebApp.EventBus.Handlers;

internal class ErrorEventHandler(IHubContext<ErrorHub> errorHub) : IConsumer<ErrorNotification>
{
    public async Task Consume(ConsumeContext<ErrorNotification> context)
    {
        await errorHub.Clients.User(context.Message.MessageContext.UserId).SendAsync("Error", context.Message.ErrorMessage, context.CancellationToken);
    }
}