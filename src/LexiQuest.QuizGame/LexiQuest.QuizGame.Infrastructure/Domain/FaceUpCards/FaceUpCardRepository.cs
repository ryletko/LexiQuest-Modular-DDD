using LexiQuest.QuizGame.Domain.FaceUpCards;
using LexiQuest.QuizGame.Domain.GameStates;
using LexiQuest.QuizGame.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.QuizGame.Infrastructure.Domain.FaceUpCards;

internal class FaceUpCardRepository(QuizGameContext dbcontext) : IFaceUpCardRepository
{
    public async Task<FaceUpCard?> GetActiveFaceUpCardForGameAsync(GameId gameId, CancellationToken cancellationToken)
    {
        return await dbcontext.FaceUpCards.FirstOrDefaultAsync(x => x.GameId == gameId && x.CompletedAt == null, cancellationToken: cancellationToken);
    }

    public async Task<FaceUpCard> GetById(FaceUpCardId faceUpCardId, CancellationToken cancellationToken)
    {
        return await dbcontext.FaceUpCards.FirstAsync(x => x.Id == faceUpCardId, cancellationToken: cancellationToken);
    }

    public async Task AddAsync(FaceUpCard faceUpCard, CancellationToken cancellationToken)
    {
        await dbcontext.AddAsync(faceUpCard, cancellationToken);
    }

    public async Task<List<FaceUpCard>> GetAllFaceUpCardsForGame(GameId gameId, CancellationToken cancellationToken)
    {
        return await dbcontext.FaceUpCards.Where(x => x.GameId == gameId).ToListAsync(cancellationToken);
    }
}