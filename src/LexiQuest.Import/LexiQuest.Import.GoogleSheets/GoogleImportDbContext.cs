using LexiQuest.Framework.Infrastructure.DataAccess;
using LexiQuest.Import.GoogleSheets.ImportSaga;
using LexiQuest.Import.GoogleSheets.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LexiQuest.Import.GoogleSheets;

// https://masstransit.io/documentation/configuration/persistence/entity-framework
internal class GoogleImportDbContext : BaseDbContext
{
    public GoogleImportDbContext()
    {
    }


    public GoogleImportDbContext(DbContextOptions<GoogleImportDbContext> options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
    {
    }

    public const string SchemaNameStatic = "googleimport";

    protected override string SchemaName => SchemaNameStatic;
    protected override string MigrationConnectionStringName => "MigrationDb";
    protected override string MigrationAppsettingsJson => "appsettings.json";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImportSagaData>()
                    .ToTable(nameof(ImportSagaData), SchemaNameStatic)
                    .HasKey(s => s.CorrelationId);

        modelBuilder.Entity<ImportSource>()
                    .ToTable(nameof(ImportSource), SchemaNameStatic);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }


    public DbSet<ImportSagaData> ImportSagaData { get; set; }

    public DbSet<ImportSource> ImportSources { get; set; }
}