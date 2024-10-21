using System.Threading.Tasks;
using LexiQuest.QuizGame.Contracts.Events;
using LexiQuest.Shared.QuizGame;
using LexiQuest.WebApp.Hubs;
using LexiQuest.WebApp.Shared.GameHub;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace LexiQuest.WebApp.EventBus.Handlers;

internal class GameEventHandler(IHubContext<GameHub> gameHub) : IConsumer<NewCardOpenedEvent>, IConsumer<GameStateUpdatedEvent>
{
    public async Task Consume(ConsumeContext<NewCardOpenedEvent> context)
    {
        await gameHub.Clients.User(context.Message.MessageContext.UserId).SendAsync("GameStateUpdated", new GameStateUpdatedDto(context.Message.GameId, GameStatus.Active, context.Message.MessageContext.CorrelationId), context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<GameStateUpdatedEvent> context)
    {
        await gameHub.Clients.User(context.Message.MessageContext.UserId).SendAsync("GameStateUpdated", new GameStateUpdatedDto(context.Message.GameId, context.Message.GameStatus, context.Message.MessageContext.CorrelationId), context.CancellationToken);
    }

}