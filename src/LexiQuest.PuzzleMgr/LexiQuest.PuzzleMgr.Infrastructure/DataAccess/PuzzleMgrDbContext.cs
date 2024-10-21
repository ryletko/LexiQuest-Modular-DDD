using LexiQuest.Framework.Infrastructure.DataAccess;
using LexiQuest.PuzzleMgr.Domain.PuzzleCollections;
using LexiQuest.PuzzleMgr.Domain.Puzzles;
using LexiQuest.PuzzleMgr.Infrastructure.Config.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LexiQuest.PuzzleMgr.Infrastructure.DataAccess;

internal class PuzzleMgrDbContext : BaseDbContext
{
    public DbSet<Puzzle> Puzzles { get; set; }
    public DbSet<PuzzleCollection> PuzzleCollections { get; set; }
    public DbSet<LanguageLevel> LanguageLevels { get; set; }

    public PuzzleMgrDbContext()
    {
    }
    
    public PuzzleMgrDbContext(DbContextOptions options, ILoggerFactory loggerFactory)
        : base(options, loggerFactory)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override string SchemaName => Schema.Name;
    protected override string MigrationConnectionStringName => "PuzzleMgr";
    protected override string MigrationAppsettingsJson => "appsettings.json";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}