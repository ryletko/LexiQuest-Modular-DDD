using LexiQuest.PuzzleMgr.Domain.PuzzleCollections;
using LexiQuest.PuzzleMgr.Domain.PuzzleOwners;

namespace LexiQuest.PuzzleMgr.Domain.Puzzles;

public interface IPuzzleRepository
{
    Task AddAsync(Puzzle puzzle, CancellationToken cancellationToken);

    Task AddAsync(IEnumerable<Puzzle> puzzles, CancellationToken cancellationToken);
    Task DeleteAsync(Puzzle puzzle, CancellationToken cancellationToken);
    Task DeleteAllForOwnerAsync(PuzzleOwnerId ownerId, CancellationToken cancellationToken);
    Task GetAll(Puzzle puzzle, CancellationToken cancellationToken);
    Task GetByCollectionId(PuzzleCollectionId collectionId, CancellationToken cancellationToken);
    Task GetById(PuzzleId puzzleId, CancellationToken cancellationToken);
}