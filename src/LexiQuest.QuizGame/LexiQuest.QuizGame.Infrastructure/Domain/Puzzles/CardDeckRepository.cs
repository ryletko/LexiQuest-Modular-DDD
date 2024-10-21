using LexiQuest.QuizGame.Domain.Decks;
using LexiQuest.QuizGame.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.QuizGame.Infrastructure.Domain.Puzzles;

internal class CardDeckRepository(QuizGameContext context) : ICardDeckRepository
{
    public async Task AddAsync(CardDeck cardDeck, CancellationToken cancellationToken)
    {
        await context.AddAsync(cardDeck, cancellationToken);
    }

    public async Task<CardDeck> GetByIdAsync(CardDeckId cardDeckId, CancellationToken cancellationToken)
    {
        // return await context.PuzzleSets.Include(x => x.CurrentPuzzle).FirstAsync(x => x.Id == puzzleSetId, cancellationToken);
        return await context.CardDecks.Include(x => x.Cards).FirstAsync(x => x.Id == cardDeckId, cancellationToken);
    }
}