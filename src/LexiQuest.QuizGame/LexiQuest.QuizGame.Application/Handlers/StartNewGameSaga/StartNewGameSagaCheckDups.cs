using LexiQuest.Framework.Application.DataAccess;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.QuizGame.Application.Handlers.StartNewGameSaga;

public class StartNewGameSagaCheckDups(IQueryContext queryContext) : IStateMachineActivity<StartNewGameSagaState>
{
    private async Task CheckAlreadyStartedForPlayer(BehaviorContext<StartNewGameSagaState> context)
    {
        var timestampThreshold = DateTimeOffset.UtcNow.AddMinutes(-1);
        var userStartGamesWithinMinute = await queryContext.Query<StartNewGameSagaState>()
                                                           .Where(x => x.PlayerId == context.Saga.PlayerId && !x.GameCreated && x.Timestamp > timestampThreshold)
                                                           .OrderBy(x => x.Timestamp)
                                                           .FirstOrDefaultAsync(context.CancellationToken);

        if (userStartGamesWithinMinute != null && userStartGamesWithinMinute.CorrelationId != context.Saga.CorrelationId)
            context.Saga.RefusedAlreadyStarted = true;
    }

    public void Probe(ProbeContext context)
    {
        context.CreateScope("PublishStartNewGameSagaInitCheck");
    }

    public void Accept(StateMachineVisitor visitor)
    {
        visitor.Visit(this);
    }

    public async Task Execute(BehaviorContext<StartNewGameSagaState> context, IBehavior<StartNewGameSagaState> next)
    {
        await CheckAlreadyStartedForPlayer(context);

        // always call the next activity in the behavior
        await next.Execute(context).ConfigureAwait(false);
    }

    public async Task Execute<T>(BehaviorContext<StartNewGameSagaState, T> context, IBehavior<StartNewGameSagaState, T> next) where T : class
    {
        await CheckAlreadyStartedForPlayer(context);

        // always call the next activity in the behavior
        await next.Execute(context).ConfigureAwait(false);
    }

    public async Task Faulted<TException>(BehaviorExceptionContext<StartNewGameSagaState, TException> context, IBehavior<StartNewGameSagaState> next) where TException : Exception
    {
        // always call the next activity in the behavior
        await next.Faulted(context);
    }

    public async Task Faulted<T, TException>(BehaviorExceptionContext<StartNewGameSagaState, T, TException> context, IBehavior<StartNewGameSagaState, T> next) where T : class where TException : Exception
    {
        // always call the next activity in the behavior
        await next.Faulted(context);
    }
}