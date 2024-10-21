using LexiQuest.Framework.Domain;
using LexiQuest.QuizGame.Domain.GameStates;

namespace LexiQuest.QuizGame.Domain.Players;

public class PlayerId(string  value) : IdentityId(value);

public class Player : Entity, IAggregateRoot
{
    public PlayerId Id { get; private init; }

    private List<GameId> _games;
    public IReadOnlyList<GameId> Games => _games.AsReadOnly();

    private Player()
    {
        _games = [];
    }

    public static Player CreateNew(PlayerId playerId)
    {
        return new Player
               {
                   Id     = playerId,
                   _games = [],
               };
    }
}