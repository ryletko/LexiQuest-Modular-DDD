using LexiQuest.Framework.Application.DataAccess;
using LexiQuest.Framework.Application.Messages.Queries;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.QuizGame.Application.Games.Access;
using LexiQuest.QuizGame.Contracts.Queries;
using LexiQuest.QuizGame.Domain.GameStates;
using LexiQuest.QuizGame.Domain.Players;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.QuizGame.Application.Handlers.Queries;

internal class GetStartedGamesQueryHandler(IQueryContext queryContext) : QueryHandlerBase<GetStartedGamesQuery, GetStartedGamesQueryResult>, IEventBusMessageHandler
{
    public override async Task<GetStartedGamesQueryResult> Handle(GetStartedGamesQuery query, CancellationToken cancellationToken = default)
    {
        var userId = query.MessageContext.UserId; 
        var startedGames = await queryContext.GetUserFilteredGames(query)
                                             .Where(x => x.Status == GameStatus.Ready || x.Status == GameStatus.Active)
                                             .Select(x => new GetStartedGamesQueryResult.GameItem(x.GameId.Value))
                                             .ToListAsync(cancellationToken);
        
        return new GetStartedGamesQueryResult(startedGames);
    }
}