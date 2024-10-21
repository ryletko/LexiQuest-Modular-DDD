using LexiQuest.PuzzleMgr.Domain.PuzzleCollections;

namespace LexiQuest.PuzzleMgr.Infrastructure.Domain.PuzzleCollections;

public class PuzzleCollectionRepository: IPuzzleCollectionRepository
{

    public async Task<PuzzleCollection> GetByIdAsync(PuzzleCollectionId puzzleCollectionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(PuzzleCollection puzzleCollection, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(PuzzleCollection puzzleCollection, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}