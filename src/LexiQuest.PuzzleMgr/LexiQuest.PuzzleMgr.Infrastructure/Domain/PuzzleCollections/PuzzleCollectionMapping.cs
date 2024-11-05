using LexiQuest.PuzzleMgr.Domain.PuzzleCollections;
using LexiQuest.PuzzleMgr.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LexiQuest.PuzzleMgr.Infrastructure.Domain.PuzzleCollections;

public class PuzzleCollectionMapping : IEntityTypeConfiguration<PuzzleCollection>
{
    public void Configure(EntityTypeBuilder<PuzzleCollection> builder)
    {
        builder.ToTable("PuzzleCollection", Schema.Name).HasKey(x => x.Id);
        builder.OwnsOne(x => x.Name,
                        x => { x.Property(y => y.Text).HasColumnName($"{nameof(PuzzleCollection.Name)}"); });
        builder.Property("_puzzles").HasColumnName("Puzzles");
    }
}