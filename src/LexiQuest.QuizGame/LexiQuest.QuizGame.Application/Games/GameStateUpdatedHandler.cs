using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.Events;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.QuizGame.Contracts.Events;
using LexiQuest.QuizGame.Domain.GameStates.Events;

namespace LexiQuest.QuizGame.Application.Games;

internal class GameStateUpdatedHandler(IMediatorEventBus eventBus): EventHandlerBase<MediatorDomainEvent<GameStateUpdated>>, IInternalMessageHandler
{
    public override async Task Handle(MediatorDomainEvent<GameStateUpdated> command, CancellationToken cancellationToken)
    {
        await eventBus.SendEvent(new GameStateUpdatedEvent(command.DomainEvent.GameId.Value, command.DomainEvent.GameStatus.ToExternalGameStatus()));
    }
}