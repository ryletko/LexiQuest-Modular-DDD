using LexiQuest.Framework.Application.Messages.Context;
using LexiQuest.PuzzleMgr.Contracts.Queries;
using LexiQuest.QuizGame.Application.Games.CreateNewGame;
using LexiQuest.QuizGame.Application.Games.StartNewGame;
using LexiQuest.QuizGame.Contracts.Commands;
using LexiQuest.QuizGame.Contracts.Events;
using MassTransit;

namespace LexiQuest.QuizGame.Application.Handlers.StartNewGameSaga;

public class StartNewGameStateMachine : MassTransitStateMachine<StartNewGameSagaState>
{
    public State CheckingLimit { get; set; }
    public State FetchingPuzzles { get; set; }
    public State CreatingNewGame { get; set; }
    public State StartingNewGame { get; set; }
    public State RefusedAlreadyStarted { get; set; }

    public Event<StartNewGameCommand> StartNewGame { get; set; }
    public Request<StartNewGameSagaState, GetPuzzlesForCurrentOwnerQuery, GetPuzzlesForCurrentOwnerQueryResult> PuzzleRequest { get; private set; } = null!;
    public Request<StartNewGameSagaState, StartNewGameCheckLimitRequest, StartNewGameCheckLimitRequestResult> CheckLimitRequest { get; private set; } = null!;
    public Event<NewGameCreatedEvent> NewGameCreatedEvent { get; set; }
    public Event<GameStartedEvent> GameStartedEvent { get; set; }

    public StartNewGameStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => StartNewGame, e => e.CorrelateById(x => x.Message.MessageContext.CorrelationId)); // one game start per user
        Request(() => CheckLimitRequest, e => e.CheckLimitRequestId);
        Request(() => PuzzleRequest, e => e.PuzzleRequestId);
        Event(() => NewGameCreatedEvent, e => e.CorrelateById(x => x.Message.MessageContext.CorrelationId));
        Event(() => GameStartedEvent, e => e.CorrelateById(x => x.Message.MessageContext.CorrelationId));

        Initially(
            When(StartNewGame)
               .Then(ctx =>
                     {
                         ctx.Saga.UserId         = ctx.Message.MessageContext.UserId;
                         ctx.Saga.StartNewGameId = ctx.Message.MessageContext.CorrelationId;
                         ctx.Saga.PlayerId       = ctx.Message.MessageContext.UserId;
                         ctx.Saga.Timestamp      = DateTimeOffset.UtcNow;
                         ctx.Saga.Initialized    = true;
                     })
               .TransitionTo(CheckingLimit)
               .Request(CheckLimitRequest, ctx => new StartNewGameCheckLimitRequest().ContextFrom(ctx.Message)));

        During(CheckingLimit,
               When(CheckLimitRequest.Completed)
                  .IfElse(ctx => ctx.Message.IsDuplicate,
                          s => s.TransitionTo(RefusedAlreadyStarted)
                                .Publish(ctx => new StartNewGameRefusedEvent(ctx.Saga.StartNewGameId).ContextFrom(ctx.Saga))
                                .Finalize(),
                          s => s.TransitionTo(FetchingPuzzles)
                                .Publish(ctx => new StartNewGameStatusEvent(ctx.Saga.StartNewGameId, ctx.Saga.CurrentState).ContextFrom(ctx.Saga))
                                .Request(PuzzleRequest, ctx => new GetPuzzlesForCurrentOwnerQuery().ContextFrom(ctx.Saga))));
        During(FetchingPuzzles,
               When(PuzzleRequest.Completed)
                  .Then(ctx => { ctx.Saga.PuzzlesFetched = true; })
                  .TransitionTo(CreatingNewGame)
                  .Publish(ctx => new StartNewGameStatusEvent(ctx.Saga.StartNewGameId, ctx.Saga.CurrentState).ContextFrom(ctx.Saga))
                  .Publish(ctx => new CreateNewGameCommand(ctx.Message.PuzzleItems.Select(x => new CreateNewGameCommand.PuzzleItem(
                                                                                              x.PuzzleId,
                                                                                              x.ForeignWord,
                                                                                              x.Language,
                                                                                              x.PartsOfSpeech,
                                                                                              x.Definitions,
                                                                                              x.Examples,
                                                                                              x.Synonims,
                                                                                              x.Transcription,
                                                                                              x.Level,
                                                                                              x.From)).ToList()).ContextFrom(ctx.Saga)));

        During(CreatingNewGame,
               When(NewGameCreatedEvent)
                  .Then(ctx =>
                        {
                            ctx.Saga.NewGameId   = ctx.Message.NewGameId;
                            ctx.Saga.GameCreated = true;
                        })
                  .Publish(ctx => new StartNewGameStatusEvent(ctx.Saga.StartNewGameId, ctx.Saga.CurrentState).ContextFrom(ctx.Saga))
                  .Publish(ctx => new StartGameCommand(ctx.Saga.NewGameId.Value).ContextFrom(ctx.Saga))
                  .TransitionTo(StartingNewGame));

        During(StartingNewGame,
               When(GameStartedEvent)
                  .Then(ctx => { ctx.Saga.GameStarted = true; })
                  .Publish(ctx => new StartNewGameCompletedEvent(ctx.Saga.StartNewGameId, ctx.Saga.NewGameId.Value).ContextFrom(ctx.Message))
                  .Finalize());

        SetCompletedWhenFinalized();
    }
}
