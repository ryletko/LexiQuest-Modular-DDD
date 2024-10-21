using LexiQuest.PuzzleMgr.Domain.PuzzleCollections;
using LexiQuest.PuzzleMgr.Domain.PuzzleOwners;
using LexiQuest.PuzzleMgr.Domain.Puzzles;
using LexiQuest.PuzzleMgr.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.PuzzleMgr.Infrastructure.Domain.Puzzles;

internal class PuzzleRepository(PuzzleMgrDbContext db) : IPuzzleRepository
{
    public async Task AddAsync(Puzzle puzzle, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(IEnumerable<Puzzle> puzzles, CancellationToken cancellationToken = default)
    {
        await db.Puzzles.AddRangeAsync(puzzles, cancellationToken);
    }

    public async Task DeleteAsync(Puzzle puzzle, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAllForOwnerAsync(PuzzleOwnerId ownerId, CancellationToken cancellationToken)
    {
        await db.Puzzles.Where(x => x.PuzzleOwnerId == ownerId).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task GetAll(Puzzle puzzle, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task GetByCollectionId(PuzzleCollectionId collectionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task GetById(PuzzleId puzzleId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}