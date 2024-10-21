using LexiQuest.PuzzleMgr.Domain.Puzzles;
using LexiQuest.PuzzleMgr.Domain.Puzzles.ForeignWords;
using LexiQuest.PuzzleMgr.Infrastructure.Config.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LexiQuest.PuzzleMgr.Infrastructure.Domain.Puzzles;

internal class PuzzleMapping : IEntityTypeConfiguration<Puzzle>
{
    public void Configure(EntityTypeBuilder<Puzzle> builder)
    {
        builder.ToTable("Puzzles", Schema.Name);
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.ForeignWord,
                        x =>
                        {
                            x.Property(y => y.Language).HasColumnName($"{nameof(Puzzle.ForeignWord)}{nameof(ForeignWord.Language)}");
                            x.Property(y => y.Text).HasColumnName($"{nameof(Puzzle.ForeignWord)}{nameof(ForeignWord.Text)}");
                        });
        builder.OwnsOne(x => x.Transcription,
                        x => x.Property(y => y.StrVal).HasColumnName(nameof(Puzzle.Transcription)));
        builder.OwnsMany(x => x.Definitions,
                         x =>
                         {
                             x.WithOwner().HasForeignKey("PuzzleId");
                             x.ToTable("Definitions");
                             x.Property(y => y.Text).HasColumnName(nameof(Definition.Text));
                         });
        builder.OwnsMany(x => x.Synonims,
                         x =>
                         {
                             x.WithOwner().HasForeignKey("PuzzleId");
                             x.ToTable("Synonims");
                             x.Property(y => y.Text).HasColumnName(nameof(ForeignWord.Text));
                         });
        builder.OwnsMany(x => x.Examples,
                         x =>
                         {
                             x.WithOwner().HasForeignKey("PuzzleId");
                             x.ToTable("Examples");
                             x.Property(y => y.Text).HasColumnName(nameof(Example.Text));
                         });
        builder.HasOne(x => x.Level)
               .WithMany();
        // .HasForeignKey(x => new {x.Level.Language, x.Level.TextRepresentation});
    }
}