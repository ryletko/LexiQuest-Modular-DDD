using LexiQuest.Framework.Application.Messages.InternalProcessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LexiQuest.Framework.Infrastructure.DataAccess.Outbox;

internal class OutboxMessageEntityTypeConfiguration (string schemaName): IEntityTypeConfiguration<OutboxMessage>
{

    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("_OutboxMessages", schemaName);

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedNever();
    }
}