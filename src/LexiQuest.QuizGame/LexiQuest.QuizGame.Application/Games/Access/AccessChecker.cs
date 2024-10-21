using System.Linq.Expressions;
using LexiQuest.Framework.Application.DataAccess;
using LexiQuest.Framework.Application.Errors;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Context;
using LexiQuest.QuizGame.Domain.GameStates;
using LexiQuest.QuizGame.Domain.Players;

namespace LexiQuest.QuizGame.Application.Games.Access;

public static class AccessChecker
{
    public static void CheckAccess(this GameState game, ICommand command, PermissionAction permissionAction)
    {
        if (game.PlayerId.Value != command.MessageContext.UserId)
            throw new AccessDeniedException(command.MessageContext.UserId, $"game {game.GameId}", permissionAction);
    }
    //
    // public static async Task CheckReadAccessAsync<T>(IQueryContext queryContext, Guid gameId, IQuery<T> command)
    // {
    //     if (await queryContext.Query<GameState>().AnyAsync(x => x.PlayerId.Value != command.MessageContext.UserId && x.GameId.Value == gameId))
    //         throw new AccessDeniedException(command.MessageContext.UserId, $"game {gameId}", PermissionAction.Read);
    // }

    public static Expression<Func<GameState, bool>> GetUserFilter(IContextedMessage msg) => g => g.PlayerId == new PlayerId(msg.MessageContext.UserId);

    public static IQueryable<GameState> GetUserFilteredGames(this IQueryContext query, IContextedMessage msg) => query.Query<GameState>().Where(GetUserFilter(msg));
}