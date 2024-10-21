using LexiQuest.QuizGame.Domain.GameStates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LexiQuest.QuizGame.Infrastructure.Domain.GamesStates;

internal class GameMapping : IEntityTypeConfiguration<GameState>
{
    public void Configure(EntityTypeBuilder<GameState> builder)
    {
        builder.ToTable("GameStates", Schema.Name).HasKey(x => x.GameId);
        builder.OwnsOne(x => x.Score, x => { x.Property(s => s.IntVal).HasColumnName("Score"); });
    }
}