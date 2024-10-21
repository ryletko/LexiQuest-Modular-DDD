using LexiQuest.Framework.Application.Messages.InternalProcessing;
using LexiQuest.Framework.Infrastructure.DataAccess.Converters;
using LexiQuest.Framework.Infrastructure.DataAccess.InternalCommands;
using LexiQuest.Framework.Infrastructure.DataAccess.Migrations;
using LexiQuest.Framework.Infrastructure.DataAccess.Outbox;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LexiQuest.Framework.Infrastructure.DataAccess;

public abstract class BaseDbContext : DbContext
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<InternalCommand> InternalCommands { get; set; }

    private readonly ILoggerFactory _loggerFactory;

    protected BaseDbContext()
    {
    }

    protected BaseDbContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
    {
        _loggerFactory = loggerFactory;
    }

    protected abstract string SchemaName { get; }
    protected abstract string MigrationConnectionStringName { get; }
    protected abstract string MigrationAppsettingsJson { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration(SchemaName));
        modelBuilder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration(SchemaName));
        
        modelBuilder.AddInboxStateEntity(c => c.ToTable("_MT_InboxState", SchemaName));
        modelBuilder.AddOutboxMessageEntity(c => c.ToTable("_MT_OutboxMessage", SchemaName));
        modelBuilder.AddOutboxStateEntity(c => c.ToTable("_MT_OutboxState", SchemaName));
        
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
            optionsBuilder.UseNpgsql(connectionString, x => x.UseMigrationTable(SchemaName));
        }

        optionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
        optionsBuilder.EnableSensitiveDataLogging();
        
        base.OnConfiguring(optionsBuilder);
    }
}