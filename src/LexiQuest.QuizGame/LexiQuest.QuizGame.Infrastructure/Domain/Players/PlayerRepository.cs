using LexiQuest.QuizGame.Domain.Players;
using LexiQuest.QuizGame.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.QuizGame.Infrastructure.Domain.Players;

internal class PlayerRepository(QuizGameContext context) : IPlayerRepository
{
    public async Task<Player?> GetByIdAsync(PlayerId playerId, CancellationToken cancellationToken)
    {
        return await context.Players.FirstOrDefaultAsync(x => x.Id == playerId, cancellationToken: cancellationToken);
    }

    public async Task AddAsync(Player player, CancellationToken cancellationToken)
    {
        await context.AddAsync(player, cancellationToken);
    }
}