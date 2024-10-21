using LexiQuest.QuizGame.Domain.GameStates;

namespace LexiQuest.QuizGame.Domain.FaceUpCards;

public interface IFaceUpCardRepository
{
    Task AddAsync(FaceUpCard faceUpCard, CancellationToken cancellationToken);
    Task<FaceUpCard?> GetActiveFaceUpCardForGameAsync(GameId gameId, CancellationToken cancellationToken);
    Task<FaceUpCard> GetById(FaceUpCardId faceUpCardId, CancellationToken cancellationToken);
    Task<List<FaceUpCard>> GetAllFaceUpCardsForGame(GameId gameId, CancellationToken cancellationToken);
}