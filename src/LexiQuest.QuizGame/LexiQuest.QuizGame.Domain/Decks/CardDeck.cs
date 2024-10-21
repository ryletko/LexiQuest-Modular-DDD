using LexiQuest.Framework.Domain;
using LexiQuest.QuizGame.Domain.Players;

namespace LexiQuest.QuizGame.Domain.Decks;

public class CardDeckId(Guid value) : TypedIdValueBase(value);

// хранит загруженые в игру паззлы
// тупо колода
public class CardDeck : Entity, IAggregateRoot
{
    public PlayerId PlayerId { get; private set; }
    public CardDeckId Id { get; private init; }

    private List<FaceDownCard> _cards;
    public IReadOnlyList<FaceDownCard> Cards => _cards.AsReadOnly();

    // public Puzzle CurrentPuzzle => _puzzles.First(x => x.IsCurrent);

    protected CardDeck()
    {
        _cards = [];
    }

    private CardDeck(IReadOnlyList<FaceDownCard> cards)
    {
        _cards = cards.ToList();
    }

    public static CardDeck CreateNew(IReadOnlyList<FaceDownCardPuzzleInfo> puzzles, PlayerId playerId)
    {
        var cards = puzzles.Select(FaceDownCard.CreateNew).ToList();
        return new CardDeck(cards)
               {
                   Id       = new CardDeckId(Guid.NewGuid()),
                   PlayerId = playerId
               };
    }
}