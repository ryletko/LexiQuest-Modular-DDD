namespace LexiQuest.PuzzleMgr.Contracts.Commands;

public class AddPuzzlesToCollectionCommand(Guid puzzleCollectionId, List<Guid> puzzleIds)
{
    public Guid PuzzleCollectionId { get; } = puzzleCollectionId;
    public List<Guid> PuzzleIds { get; } = puzzleIds;
}