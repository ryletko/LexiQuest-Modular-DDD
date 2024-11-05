using LexiQuest.Framework.Application.Messages.Events;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.QuizGame.Domain.FaceUpCards.Events;
using LexiQuest.QuizGame.Domain.GameStates;

namespace LexiQuest.QuizGame.Application.Games;

internal class FaceUpCardProcessedEventHandler(IGameStateRepository gameStateRepository) : EventHandlerBase<MediatorDomainEvent<FaceUpCardProcessed>>, IInternalMessageHandler
{
    public override async Task Handle(MediatorDomainEvent<FaceUpCardProcessed> command, CancellationToken cancellationToken)
    {
        var gameState = await gameStateRepository.GetByIdAsync(command.DomainEvent.GameId, cancellationToken);
        gameState.ApplyResult(command.DomainEvent.CardCheckStatusEnum);
    }
}