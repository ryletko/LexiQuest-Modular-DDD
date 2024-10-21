using System.Threading.Tasks;
using LexiQuest.QuizGame.Contracts.Events;
using LexiQuest.WebApp.Data;
using LexiQuest.WebApp.Hubs;
using LexiQuest.WebApp.Shared.QuizGame;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.WebApp.EventBus.Handlers;

internal class StartNewGameRefusedHandler(WebAppDbContext db,
                                          IHubContext<StartNewGameStateHub> startGameHub) : IConsumer<StartNewGameRefusedEvent>, IConsumer<StartNewGameCompletedEvent>, IConsumer<StartNewGameStatusEvent>
{
    private async Task SendToHub(StartNewGameStatus status)
    {
        await startGameHub.Clients.User(status.PlayerId).SendAsync("StartNewGameStatusUpdate",
                                                                   new StartNewGameStatusDto()
                                                                   {
                                                                       RequestedAt = status.Timestamp,
                                                                       Status      = status.Status,
                                                                       Completed   = status.Completed,
                                                                       Refused     = status.Refused,
                                                                       GameId      = status.GameId
                                                                   });
    }

    public async Task Consume(ConsumeContext<StartNewGameRefusedEvent> context)
    {
        var startNewGameStatus = await db.StartNewGameStatuses.FirstOrDefaultAsync(x => x.Id == context.Message.StartNewGameId);
        if (startNewGameStatus != null)
        {
            startNewGameStatus.Status  = "Refused";
            startNewGameStatus.Refused = true;
            await db.SaveChangesAsync(context.CancellationToken);
            await SendToHub(startNewGameStatus);
        }
    }

    public async Task Consume(ConsumeContext<StartNewGameCompletedEvent> context)
    {
        var startNewGameStatus = await db.StartNewGameStatuses.FirstOrDefaultAsync(x => x.Id == context.Message.StartNewGameId);
        if (startNewGameStatus != null)
        {
            startNewGameStatus.Status    = "Completed";
            startNewGameStatus.Completed = true;
            startNewGameStatus.GameId    = context.Message.NewGameId;
            await db.SaveChangesAsync(context.CancellationToken);
            await SendToHub(startNewGameStatus);
        }
    }

    public async Task Consume(ConsumeContext<StartNewGameStatusEvent> context)
    {
        var startNewGameStatus = await db.StartNewGameStatuses.FirstOrDefaultAsync(x => x.Id == context.Message.StartNewGameId);
        if (startNewGameStatus != null)
        {
            startNewGameStatus.Status = context.Message.Status;
            await db.SaveChangesAsync(context.CancellationToken);
            await SendToHub(startNewGameStatus);
        }
    }
}