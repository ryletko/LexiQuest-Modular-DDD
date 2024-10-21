using LexiQuest.QuizGame.Domain.GameStates;
using LexiQuest.QuizGame.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.QuizGame.Infrastructure.Domain.GamesStates;

internal class GameStateRepository(QuizGameContext context) : IGameStateRepository
{
    public async Task<GameState> GetByIdAsync(GameId gameId, CancellationToken cancellationToken)
    {
        return await context.Games.FirstAsync(x => x.GameId == gameId, cancellationToken);
    }
    
    public async Task AddAsync(GameState gameState, CancellationToken cancellationToken)
    {
        await context.Games.AddAsync(gameState, cancellationToken);
    }
}
