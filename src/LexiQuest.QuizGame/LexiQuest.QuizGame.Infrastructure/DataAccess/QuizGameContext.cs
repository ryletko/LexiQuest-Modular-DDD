using LexiQuest.Framework.Infrastructure.DataAccess;
using LexiQuest.QuizGame.Application.Handlers.StartNewGameSaga;
using LexiQuest.QuizGame.Domain.Decks;
using LexiQuest.QuizGame.Domain.FaceUpCards;
using LexiQuest.QuizGame.Domain.GameStates;
using LexiQuest.QuizGame.Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LexiQuest.QuizGame.Infrastructure.DataAccess;

internal class QuizGameContext : BaseDbContext
{
    public DbSet<GameState> Games { get; set; }
    public DbSet<CardDeck> CardDecks { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<FaceUpCard> FaceUpCards { get; set; }
    public DbSet<StartNewGameSagaState> StartNewGameSagaState { get; set; }

    public QuizGameContext()
    {
    }

    public QuizGameContext(DbContextOptions options, ILoggerFactory loggerFactory)
        : base(options, loggerFactory)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override string SchemaName => Schema.Name;
    protected override string MigrationConnectionStringName => "MigrationDb";
    protected override string MigrationAppsettingsJson => "appsettings.json";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StartNewGameSagaState>()
                    .ToTable(nameof(StartNewGameSagaState), SchemaName)
                    .HasKey(s => s.CorrelationId);

        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
