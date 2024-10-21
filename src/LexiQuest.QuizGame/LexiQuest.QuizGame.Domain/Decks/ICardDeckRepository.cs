namespace LexiQuest.QuizGame.Domain.Decks;

public interface ICardDeckRepository
{
    Task AddAsync(CardDeck cardDeck, CancellationToken cancellationToken);
    Task<CardDeck> GetByIdAsync(CardDeckId cardDeckId, CancellationToken cancellationToken);
}