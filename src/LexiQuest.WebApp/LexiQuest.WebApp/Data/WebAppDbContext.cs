using System;
using System.IO;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Utils.Core;

namespace LexiQuest.WebApp.Data;

public class WebAppDbContext : DbContext
{
    public DbSet<ImportStatus> ImportStatuses { get; set; }
    public DbSet<StartNewGameStatus> StartNewGameStatuses { get; set; }

    public WebAppDbContext()
    {
    }

    public WebAppDbContext(DbContextOptions options) : base(options)
    {
    }


    public const string SchemaName = "webapp";
    private const string MigrationConnectionStringName = "LexiQuest";
    private const string MigrationAppsettingsJson = "appsettings.json";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImportStatus>()
                    .ToTable(nameof(ImportStatus), SchemaName)
                    .Apply(m => m.HasKey(x => x.Id))
                    .Apply(m => m.Property(nameof(ImportStatus.Timestamp)))
                    .Apply(m => m.Property(nameof(ImportStatus.Status)))
                    .Apply(m => m.Property(nameof(ImportStatus.Completed)))
                    .Apply(m => m.Property(nameof(ImportStatus.ImporterId)));
        
        modelBuilder.Entity<StartNewGameStatus>()
                    .ToTable(nameof(StartNewGameStatus), SchemaName)
                    .Apply(m => m.HasKey(x => x.Id))
                    .Apply(m => m.Property(nameof(StartNewGameStatus.Timestamp)))
                    .Apply(m => m.Property(nameof(StartNewGameStatus.Status)))
                    .Apply(m => m.Property(nameof(StartNewGameStatus.Completed)))
                    .Apply(m => m.Property(nameof(StartNewGameStatus.Refused)))
                    .Apply(m => m.Property(nameof(StartNewGameStatus.GameId)))
                    .Apply(m => m.Property(nameof(StartNewGameStatus.PlayerId)));

        modelBuilder.AddInboxStateEntity(c => c.ToTable("_MB_InboxState", SchemaName));
        modelBuilder.AddOutboxMessageEntity(c => c.ToTable("_MB_OutboxMessage", SchemaName));
        modelBuilder.AddOutboxStateEntity(c => c.ToTable("_MB_OutboxState", SchemaName));

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                              .SetBasePath(Directory.GetCurrentDirectory())
                                              .AddJsonFile(MigrationAppsettingsJson)
                                              .Build();
            var connectionString = configuration.GetConnectionString(MigrationConnectionStringName);
            optionsBuilder.UseNpgsql(connectionString, x => x.MigrationsHistoryTable("__migrations", SchemaName));
        }

        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }
}