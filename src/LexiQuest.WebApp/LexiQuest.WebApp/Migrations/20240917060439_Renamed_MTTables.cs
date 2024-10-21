using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexiQuest.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Renamed_MTTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EBOutboxState",
                schema: "webapp",
                table: "EBOutboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EBOutboxMessage",
                schema: "webapp",
                table: "EBOutboxMessage");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_EBInboxState_MessageId_ConsumerId",
                schema: "webapp",
                table: "EBInboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EBInboxState",
                schema: "webapp",
                table: "EBInboxState");

            migrationBuilder.RenameTable(
                name: "EBOutboxState",
                schema: "webapp",
                newName: "_MB_OutboxState",
                newSchema: "webapp");

            migrationBuilder.RenameTable(
                name: "EBOutboxMessage",
                schema: "webapp",
                newName: "_MB_OutboxMessage",
                newSchema: "webapp");

            migrationBuilder.RenameTable(
                name: "EBInboxState",
                schema: "webapp",
                newName: "_MB_InboxState",
                newSchema: "webapp");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxState_Created",
                schema: "webapp",
                table: "_MB_OutboxState",
                newName: "IX__MB_OutboxState_Created");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxMessage_OutboxId_SequenceNumber",
                schema: "webapp",
                table: "_MB_OutboxMessage",
                newName: "IX__MB_OutboxMessage_OutboxId_SequenceNumber");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumb~",
                schema: "webapp",
                table: "_MB_OutboxMessage",
                newName: "IX__MB_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNu~");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxMessage_ExpirationTime",
                schema: "webapp",
                table: "_MB_OutboxMessage",
                newName: "IX__MB_OutboxMessage_ExpirationTime");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxMessage_EnqueueTime",
                schema: "webapp",
                table: "_MB_OutboxMessage",
                newName: "IX__MB_OutboxMessage_EnqueueTime");

            migrationBuilder.RenameIndex(
                name: "IX_EBInboxState_Delivered",
                schema: "webapp",
                table: "_MB_InboxState",
                newName: "IX__MB_InboxState_Delivered");

            migrationBuilder.AddPrimaryKey(
                name: "PK__MB_OutboxState",
                schema: "webapp",
                table: "_MB_OutboxState",
                column: "OutboxId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__MB_OutboxMessage",
                schema: "webapp",
                table: "_MB_OutboxMessage",
                column: "SequenceNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK__MB_InboxState_MessageId_ConsumerId",
                schema: "webapp",
                table: "_MB_InboxState",
                columns: new[] { "MessageId", "ConsumerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__MB_InboxState",
                schema: "webapp",
                table: "_MB_InboxState",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__MB_OutboxState",
                schema: "webapp",
                table: "_MB_OutboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK__MB_OutboxMessage",
                schema: "webapp",
                table: "_MB_OutboxMessage");

            migrationBuilder.DropUniqueConstraint(
                name: "AK__MB_InboxState_MessageId_ConsumerId",
                schema: "webapp",
                table: "_MB_InboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK__MB_InboxState",
                schema: "webapp",
                table: "_MB_InboxState");

            migrationBuilder.RenameTable(
                name: "_MB_OutboxState",
                schema: "webapp",
                newName: "EBOutboxState",
                newSchema: "webapp");

            migrationBuilder.RenameTable(
                name: "_MB_OutboxMessage",
                schema: "webapp",
                newName: "EBOutboxMessage",
                newSchema: "webapp");

            migrationBuilder.RenameTable(
                name: "_MB_InboxState",
                schema: "webapp",
                newName: "EBInboxState",
                newSchema: "webapp");

            migrationBuilder.RenameIndex(
                name: "IX__MB_OutboxState_Created",
                schema: "webapp",
                table: "EBOutboxState",
                newName: "IX_EBOutboxState_Created");

            migrationBuilder.RenameIndex(
                name: "IX__MB_OutboxMessage_OutboxId_SequenceNumber",
                schema: "webapp",
                table: "EBOutboxMessage",
                newName: "IX_EBOutboxMessage_OutboxId_SequenceNumber");

            migrationBuilder.RenameIndex(
                name: "IX__MB_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNu~",
                schema: "webapp",
                table: "EBOutboxMessage",
                newName: "IX_EBOutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumb~");

            migrationBuilder.RenameIndex(
                name: "IX__MB_OutboxMessage_ExpirationTime",
                schema: "webapp",
                table: "EBOutboxMessage",
                newName: "IX_EBOutboxMessage_ExpirationTime");

            migrationBuilder.RenameIndex(
                name: "IX__MB_OutboxMessage_EnqueueTime",
                schema: "webapp",
                table: "EBOutboxMessage",
                newName: "IX_EBOutboxMessage_EnqueueTime");

            migrationBuilder.RenameIndex(
                name: "IX__MB_InboxState_Delivered",
                schema: "webapp",
                table: "EBInboxState",
                newName: "IX_EBInboxState_Delivered");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EBOutboxState",
                schema: "webapp",
                table: "EBOutboxState",
                column: "OutboxId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EBOutboxMessage",
                schema: "webapp",
                table: "EBOutboxMessage",
                column: "SequenceNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EBInboxState_MessageId_ConsumerId",
                schema: "webapp",
                table: "EBInboxState",
                columns: new[] { "MessageId", "ConsumerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EBInboxState",
                schema: "webapp",
                table: "EBInboxState",
                column: "Id");
        }
    }
}
