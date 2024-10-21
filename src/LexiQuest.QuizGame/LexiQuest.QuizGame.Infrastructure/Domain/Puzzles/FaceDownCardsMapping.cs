using LexiQuest.QuizGame.Domain.Decks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LexiQuest.QuizGame.Infrastructure.Domain.Puzzles;

public class FaceDownCardsMapping : IEntityTypeConfiguration<FaceDownCard>
{
    private const string FaceDownCardsTable = "FaceDownCards";

    public void Configure(EntityTypeBuilder<FaceDownCard> builder)
    {
        builder.ToTable(FaceDownCardsTable, Schema.Name).HasKey(x => x.Id);
        builder.OwnsOne(p => p.FaceDownCardPuzzleInfo,
                        p =>
                        {
                            p.Property(y => y.ForeignWord).HasColumnName(nameof(FaceDownCardPuzzleInfo.ForeignWord));
                            p.Property(y => y.PartsOfSpeech).HasColumnName(nameof(FaceDownCardPuzzleInfo.PartsOfSpeech));
                            p.Property(y => y.Transcription).HasColumnName(nameof(FaceDownCardPuzzleInfo.Transcription));
                            p.Property(y => y.From).HasColumnName(nameof(FaceDownCardPuzzleInfo.From));
                            p.Property(y => y.Language).HasColumnName(nameof(FaceDownCardPuzzleInfo.Language));
                            p.Property("_definitions").HasColumnName(nameof(FaceDownCardPuzzleInfo.Definitions)); //todo with ef core 9 realease change it to property mapping, in ef core 8 read-only primitive collections are not supported  
                            p.Property("_synonims").HasColumnName(nameof(FaceDownCardPuzzleInfo.Synonims));
                            p.Property("_examples").HasColumnName(nameof(FaceDownCardPuzzleInfo.Examples));
                            p.Property(y => y.Level).HasColumnName(nameof(FaceDownCardPuzzleInfo.Level));
                        });
        // builder.Navigation(x => x.);
        // HasOne<PuzzleSet>()
        //    .WithOne(x => x.CurrentPuzzle)
        //    .IsRequired(false);

    }
}