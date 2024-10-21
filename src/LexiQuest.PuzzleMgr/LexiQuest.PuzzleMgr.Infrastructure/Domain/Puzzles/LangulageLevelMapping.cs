using LexiQuest.PuzzleMgr.Domain.Puzzles;
using LexiQuest.PuzzleMgr.Infrastructure.Config.DataAccess;
using LexiQuest.Shared.Puzzle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LexiQuest.PuzzleMgr.Infrastructure.Domain.Puzzles;

internal class LangulageLevelMapping : IEntityTypeConfiguration<LanguageLevel>
{
    public void Configure(EntityTypeBuilder<LanguageLevel> builder)
    {
        builder.ToTable("LanguageLevel", Schema.Name);
        builder.HasData(new {Language = Language.English, TextRepresentation = "A1"},
                        new {Language = Language.English, TextRepresentation = "A2"},
                        new {Language = Language.English, TextRepresentation = "B1"},
                        new {Language = Language.English, TextRepresentation = "B2"},
                        new {Language = Language.English, TextRepresentation = "C1"},
                        new {Language = Language.German, TextRepresentation = "A1"},
                        new {Language = Language.German, TextRepresentation = "A1.1"},
                        new {Language = Language.German, TextRepresentation = "A1.2"},
                        new {Language = Language.German, TextRepresentation = "A2"},
                        new {Language = Language.German, TextRepresentation = "A2.1"},
                        new {Language = Language.German, TextRepresentation = "A2.2"},
                        new {Language = Language.German, TextRepresentation = "B1"},
                        new {Language = Language.German, TextRepresentation = "B1.1"},
                        new {Language = Language.German, TextRepresentation = "B1.2"},
                        new {Language = Language.German, TextRepresentation = "B2"},
                        new {Language = Language.German, TextRepresentation = "B2.1"},
                        new {Language = Language.German, TextRepresentation = "B2.2"},
                        new {Language = Language.German, TextRepresentation = "C1"},
                        new {Language = Language.German, TextRepresentation = "C1.1"},
                        new {Language = Language.German, TextRepresentation = "C1.2"});

        builder.HasKey(x => new {x.Language, x.TextRepresentation});
    }
}