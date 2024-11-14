using System.Threading.Tasks;
using LexiQuest.PuzzleMgr.Contracts.Events;
using LexiQuest.WebApp.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace LexiQuest.WebApp.EventBus.Handlers;

internal class PuzzlesEventHandler(IHubContext<PuzzlesHub> puzzlesHub) : IConsumer<AllPuzzlesDeletedForOwner>
{
    public async Task Consume(ConsumeContext<AllPuzzlesDeletedForOwner> context)
    {
        await puzzlesHub.Clients.User(context.Message.OwnerId).SendAsync("AllPuzzlesDeleted", context.CancellationToken);
    }
}