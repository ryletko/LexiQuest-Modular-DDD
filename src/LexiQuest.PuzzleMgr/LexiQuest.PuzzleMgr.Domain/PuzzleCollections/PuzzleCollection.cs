using LexiQuest.Framework.Domain;
using LexiQuest.PuzzleMgr.Domain.Puzzles;

namespace LexiQuest.PuzzleMgr.Domain.PuzzleCollections;

public class PuzzleCollectionId(Guid value) : TypedIdValueBase(value);

public class PuzzleCollection : Entity, IAggregateRoot
{
    public PuzzleCollectionId Id { get; private set; }

    private List<PuzzleId> _puzzles;
    public IReadOnlyList<PuzzleId> Puzzles => _puzzles.AsReadOnly();

    public DateTimeOffset CreatedAt { get; private set; }

    public CollectionName Name { get; private set; }

    private PuzzleCollection()
    {
        _puzzles = [];
    }

    public static PuzzleCollection CreateNew(CollectionName collectionName)
    {
        return new PuzzleCollection()
               {
                   Id      = new PuzzleCollectionId(Guid.NewGuid()),
                   CreatedAt = SystemClock.Now,
                   Name    = collectionName
               };
    }
}