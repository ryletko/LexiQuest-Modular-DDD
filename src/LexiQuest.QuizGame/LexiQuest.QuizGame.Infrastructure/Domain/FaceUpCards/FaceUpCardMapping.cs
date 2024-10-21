using LexiQuest.QuizGame.Domain.FaceUpCards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LexiQuest.QuizGame.Infrastructure.Domain.FaceUpCards;

internal class FaceUpCardMapping : IEntityTypeConfiguration<FaceUpCard>
{
    private const string Table = "FaceUpCards";

    public void Configure(EntityTypeBuilder<FaceUpCard> builder)
    {
        builder.ToTable(Table, Schema.Name).HasKey(x => x.Id);
        
        builder.OwnsOne(p => p.PuzzleInfo,
                        p =>
                        {
                            p.Property(y => y.ForeignWord).HasColumnName(nameof(FaceUpCardPuzzleInfo.ForeignWord));
                            p.Property(y => y.PartsOfSpeech).HasColumnName(nameof(FaceUpCardPuzzleInfo.PartsOfSpeech));
                            p.Property(y => y.Transcription).HasColumnName(nameof(FaceUpCardPuzzleInfo.Transcription));
                            p.Property(y => y.From).HasColumnName(nameof(FaceUpCardPuzzleInfo.From));
                            p.Property(y => y.Language).HasColumnName(nameof(FaceUpCardPuzzleInfo.Language));
                            p.Property("_definitions").HasColumnName(nameof(FaceUpCardPuzzleInfo.Definitions)); //todo with ef core 9 realease change it to property mapping, in ef core 8 read-only primitive collections are not supported  
                            p.Property("_synonims").HasColumnName(nameof(FaceUpCardPuzzleInfo.Synonims));
                            p.Property("_examples").HasColumnName(nameof(FaceUpCardPuzzleInfo.Examples));
                            p.Property(y => y.Level).HasColumnName(nameof(FaceUpCardPuzzleInfo.Level));
                        });
    }
}