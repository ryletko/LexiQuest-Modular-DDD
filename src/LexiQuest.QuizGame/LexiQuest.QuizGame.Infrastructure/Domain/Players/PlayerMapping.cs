using LexiQuest.QuizGame.Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LexiQuest.QuizGame.Infrastructure.Domain.Players;

internal class GamesEfConfig : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("Player", Schema.Name).HasKey(x => x.Id);
        builder.Property("_games").HasColumnName("Games");
    }
}