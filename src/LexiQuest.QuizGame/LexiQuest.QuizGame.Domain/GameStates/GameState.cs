using LexiQuest.Framework.Domain;
using LexiQuest.QuizGame.Domain.Decks;
using LexiQuest.QuizGame.Domain.FaceUpCards;
using LexiQuest.QuizGame.Domain.GameStates.Events;
using LexiQuest.QuizGame.Domain.GameStates.Rules;
using LexiQuest.QuizGame.Domain.Players;

namespace LexiQuest.QuizGame.Domain.GameStates;

// сущности Game нет, так как всё что тут происходит это Game, но есть GameId с помощью которого объединяются сущности между собой
public class GameId(Guid value) : TypedIdValueBase(value);

//:: For scores and common game flow
//ii Important rule: avoid using aggregates as dumb containers
// хранит ход игры, считает счет и другую статистику, если нужно    
public class GameState : Entity, IAggregateRoot
{
    public GameId GameId { get; private set; }
    public PlayerId PlayerId { get; private set; }
    public DateTimeOffset CreatedTimestamp { get; private init; }

    public Score Score { get; private set; }
    public GameStatus Status { get; private set; }
    public CardDeckId CardDeckId { get; private init; }

    private GameState()
    {
    }

    public static GameState CreateNewGame(CardDeckId cardDeckId, PlayerId playerId) =>
        new()
        {
            GameId           = new GameId(Guid.NewGuid()),
            CreatedTimestamp = SystemClock.Now,
            PlayerId         = playerId,
            Score            = Score.FromInt(0),
            CardDeckId       = cardDeckId,
            Status           = GameStatus.Ready
        };

    public void Start()
    {
        BusinessRule.Check(new GameIsReadyRule(Status));

        Status = GameStatus.Active;
        AddDomainEvent(new GameStateUpdated(GameId, Status));
    }

    public void ApplyResult(FaceUpCardCheckResult cardCheckResult)
    {
        BusinessRule.Check(new GameIsActiveRule(Status));
        Score = Score.ApplyTurnResult(cardCheckResult);
        AddDomainEvent(new GameStateUpdated(GameId, Status));
    }

    public void Finish()
    {
        BusinessRule.Check(new GameIsActiveRule(Status));

        Status = GameStatus.Completed;
        AddDomainEvent(new GameStateUpdated(GameId, Status));
    }
}
