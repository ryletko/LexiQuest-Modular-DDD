using LexiQuest.PuzzleMgr.Domain.PuzzleOwners;
using LexiQuest.PuzzleMgr.Domain.Puzzles;

namespace LexiQuest.PuzzleMgr.Application.Commands;

internal class AddNewPuzzlesToOwner(PuzzleOwnerId puzzleOwnerId, List<PuzzleId> puzzleIds)
{
    public PuzzleOwnerId PuzzleOwnerId { get; } = puzzleOwnerId;
    public List<PuzzleId> PuzzleIds { get; } = puzzleIds;
}