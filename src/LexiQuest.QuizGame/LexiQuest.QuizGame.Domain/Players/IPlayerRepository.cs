namespace LexiQuest.QuizGame.Domain.Players;

public interface IPlayerRepository
{
    Task<Player?> GetByIdAsync(PlayerId playerId, CancellationToken cancellationToken);
    Task AddAsync(Player player, CancellationToken cancellationToken);
}