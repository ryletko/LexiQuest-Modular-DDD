namespace LexiQuest.QuizGame.Domain.GameStates;

public interface IGameStateRepository
{
    Task<GameState> GetByIdAsync(GameId gameId, CancellationToken cancellationToken);
    Task AddAsync(GameState gameState, CancellationToken cancellationToken);
}