using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.PuzzleMgr.Contracts.Queries;

namespace LexiQuest.QuizGame.Application.Handlers.StartNewGameSaga;

// это нужно чтобы конвертировать Query в Command + Event для саги 
internal record FetchPuzzlesCommand : CommandBase;
internal record PuzzlesFetched(List<GetPuzzlesForCurrentOwnerQueryResult.PuzzleItem> PuzzleItems) : CommandBase;

internal class FetchPuzzlesCommandHandler(IEventBus eventBus): CommandHandlerBase<FetchPuzzlesCommand> 
{
    public override async Task Handle(FetchPuzzlesCommand command, CancellationToken cancellationToken)
    {
        // var puzzles = await eventBus.Query<GetPuzzlesForCurrentOwnerQuery, GetPuzzlesForCurrentOwnerQueryResult>(new GetPuzzlesForCurrentOwnerQuery());
        // eventBus.SendEvent(new )
    }
}