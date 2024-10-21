using LexiQuest.QuizGame.Domain.Decks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LexiQuest.QuizGame.Infrastructure.Domain.Puzzles;

internal class PuzzleDeckMapping : IEntityTypeConfiguration<CardDeck>
{
    private const string CardDecksTable = "CardDecks";

    public void Configure(EntityTypeBuilder<CardDeck> builder)
    {
        builder.ToTable(CardDecksTable, Schema.Name)
               .HasKey(x => x.Id);
        builder.HasMany(x => x.Cards)
               .WithOne()
               .HasForeignKey("DeckId");
        // builder.Navigation(x => x.CurrentPuzzle)
        //        .Apply(x => x.Metadata.)
        //        .IsRequired(false);

        // TODO надо выяснить как всё такие можно сделать так 
        // before
        // builder.HasOne(x => x.CurrentPuzzle)
        //        .WithOne()
        //        .HasForeignKey<Puzzle>("CurrentForPuzzleSetId")
        //        .IsRequired(false);
        // builder.Ignore(x => x.CurrentPuzzle);

        //        .HasForeignKey<PuzzleSet>("CurrentPuzzleId")
        //        .Apply(x => x.Metadata.DeleteBehavior = DeleteBehavior.NoAction)
        //        .IsRequired(false);
    }
}