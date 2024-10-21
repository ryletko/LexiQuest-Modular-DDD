namespace LexiQuest.PuzzleMgr.Domain.PuzzleCollections;

public interface IPuzzleCollectionRepository
{
    Task<PuzzleCollection> GetByIdAsync(PuzzleCollectionId puzzleCollectionId, CancellationToken cancellationToken);
    Task AddAsync(PuzzleCollection puzzleCollection, CancellationToken cancellationToken);
    Task UpdateAsync(PuzzleCollection puzzleCollection, CancellationToken cancellationToken);
}