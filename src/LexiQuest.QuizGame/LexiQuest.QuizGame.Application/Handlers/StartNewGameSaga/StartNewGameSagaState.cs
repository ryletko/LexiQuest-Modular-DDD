using LexiQuest.Framework.Application.Messages.Context;
using MassTransit;

namespace LexiQuest.QuizGame.Application.Handlers.StartNewGameSaga;

public class StartNewGameSagaState: SagaStateMachineInstance, IContextedSagaState
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public string UserId { get; set; }

    public DateTimeOffset Timestamp { get; set; }
    
    public Guid StartNewGameId { get; set; }
    public Guid? CheckLimitRequestId { get; set; }

    public Guid? PuzzleRequestId { get; set; }
    public string? PlayerId { get; set; }
    public Guid? NewGameId { get; set; }

    public bool Initialized { get; set; }
    public bool PuzzlesFetched { get; set; }
    public bool GameCreated { get; set; }
    public bool GameStarted { get; set; }
    public bool RefusedAlreadyStarted { get; set; }
}
