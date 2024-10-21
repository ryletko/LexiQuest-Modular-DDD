using LexiQuest.Framework.Application.Messages.Queries;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.PuzzleMgr.Contracts.Queries;

public record GetPuzzlesForCurrentOwnerQuery : QueryBase<GetPuzzlesForCurrentOwnerQueryResult>
{
}

public record GetPuzzlesForCurrentOwnerQueryResult
{
    public List<PuzzleItem> PuzzleItems { get; }

    private GetPuzzlesForCurrentOwnerQueryResult()
    {
        
    }
    
    public GetPuzzlesForCurrentOwnerQueryResult(List<GetPuzzlesForCurrentOwnerQueryResult.PuzzleItem> PuzzleItems)
    {
        this.PuzzleItems = PuzzleItems;
    }
    
    public record PuzzleItem(Guid PuzzleId,
                             string ForeignWord,
                             Language Language,
                             PartsOfSpeech PartsOfSpeech,
                             List<string> Definitions,
                             List<string> Examples,
                             List<string> Synonims,
                             string? Transcription,
                             string? Level,
                             string? From);
}