using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.Events;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.QuizGame.Contracts.Events;
using LexiQuest.QuizGame.Domain.Decks;
using LexiQuest.QuizGame.Domain.FaceUpCards;
using LexiQuest.QuizGame.Domain.FaceUpCards.CardPicking;
using LexiQuest.QuizGame.Domain.FaceUpCards.Events;
using LexiQuest.QuizGame.Domain.GameStates;

namespace LexiQuest.QuizGame.Application.FaceUpCards;

internal class FaceUpCardCompletedEventHandler(IGameStateRepository gameStateRepository,
                                               ICardDeckRepository cardDeckRepository,
                                               IFaceUpCardRepository faceUpCardRepository,
                                               IMediatorEventBus eventBus) : EventHandlerBase<MediatorDomainEvent<FaceUpCardCompleted>>, IInternalMessageHandler
{
    public override async Task Handle(MediatorDomainEvent<FaceUpCardCompleted> command, CancellationToken cancellationToken)
    {
        var gameId = command.DomainEvent.GameId;
        var game = await gameStateRepository.GetByIdAsync(gameId, cancellationToken);
        var puzzles = await cardDeckRepository.GetByIdAsync(game.CardDeckId, cancellationToken);
        var faceUpCards = await faceUpCardRepository.GetAllFaceUpCardsForGame(gameId, cancellationToken);
        var faceUpCard = FaceUpCard.CreateNew(new RandomLessSolvedCardPicker(puzzles.Cards, faceUpCards), gameId, game.PlayerId);
        await faceUpCardRepository.AddAsync(faceUpCard, cancellationToken);
        await eventBus.SendEvent(new NewCardOpenedEvent(gameId.Value, faceUpCard.Id.Value));
    }
}