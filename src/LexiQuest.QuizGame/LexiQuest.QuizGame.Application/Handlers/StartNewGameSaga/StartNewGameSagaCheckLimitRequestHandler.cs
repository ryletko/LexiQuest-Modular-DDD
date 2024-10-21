using LexiQuest.Framework.Application.DataAccess;
using LexiQuest.Framework.Application.Messages.Queries;
using LexiQuest.Framework.Application.Messages.Registration;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.QuizGame.Application.Handlers.StartNewGameSaga;

// TODO надо найти способ получше сделать это
internal class StartNewGameSagaCheckLimitRequestHandler(IQueryContext queryContext) : QueryHandlerBase<StartNewGameCheckLimitRequest, StartNewGameCheckLimitRequestResult>, IEventBusMessageHandler
{
    public override async Task<StartNewGameCheckLimitRequestResult> Handle(StartNewGameCheckLimitRequest query, CancellationToken cancellationToken = default)
    {
        var timestampThreshold = DateTimeOffset.UtcNow.AddMinutes(-1);
        var userStartGamesWithinMinute = await queryContext.Query<StartNewGameSagaState>()
                                                           .Where(x => x.PlayerId == query.MessageContext.UserId && !x.GameCreated && x.Timestamp > timestampThreshold)
                                                           .OrderBy(x => x.Timestamp)
                                                           .FirstOrDefaultAsync(cancellationToken);

        if (userStartGamesWithinMinute != null && userStartGamesWithinMinute.CorrelationId != query.MessageContext.CorrelationId)
            return new StartNewGameCheckLimitRequestResult(true);

        return new StartNewGameCheckLimitRequestResult(false);
    }
}