using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexiQuest.Import.GoogleSheets.Migrations
{
    /// <inheritdoc />
    public partial class RenamedServiceTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OutboxMessages",
                schema: "googleimport",
                table: "OutboxMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InternalCommands",
                schema: "googleimport",
                table: "InternalCommands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EBOutboxState",
                schema: "googleimport",
                table: "EBOutboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EBOutboxMessage",
                schema: "googleimport",
                table: "EBOutboxMessage");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_EBInboxState_MessageId_ConsumerId",
                schema: "googleimport",
                table: "EBInboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EBInboxState",
                schema: "googleimport",
                table: "EBInboxState");

            migrationBuilder.RenameTable(
                name: "OutboxMessages",
                schema: "googleimport",
                newName: "_OutboxMessages",
                newSchema: "googleimport");

            migrationBuilder.RenameTable(
                name: "InternalCommands",
                schema: "googleimport",
                newName: "_InternalCommands",
                newSchema: "googleimport");

            migrationBuilder.RenameTable(
                name: "EBOutboxState",
                schema: "googleimport",
                newName: "_MT_OutboxState",
                newSchema: "googleimport");

            migrationBuilder.RenameTable(
                name: "EBOutboxMessage",
                schema: "googleimport",
                newName: "_MT_OutboxMessage",
                newSchema: "googleimport");

            migrationBuilder.RenameTable(
                name: "EBInboxState",
                schema: "googleimport",
                newName: "_MT_InboxState",
                newSchema: "googleimport");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxState_Created",
                schema: "googleimport",
                table: "_MT_OutboxState",
                newName: "IX__MT_OutboxState_Created");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxMessage_OutboxId_SequenceNumber",
                schema: "googleimport",
                table: "_MT_OutboxMessage",
                newName: "IX__MT_OutboxMessage_OutboxId_SequenceNumber");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumb~",
                schema: "googleimport",
                table: "_MT_OutboxMessage",
                newName: "IX__MT_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNu~");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxMessage_ExpirationTime",
                schema: "googleimport",
                table: "_MT_OutboxMessage",
                newName: "IX__MT_OutboxMessage_ExpirationTime");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxMessage_EnqueueTime",
                schema: "googleimport",
                table: "_MT_OutboxMessage",
                newName: "IX__MT_OutboxMessage_EnqueueTime");

            migrationBuilder.RenameIndex(
                name: "IX_EBInboxState_Delivered",
                schema: "googleimport",
                table: "_MT_InboxState",
                newName: "IX__MT_InboxState_Delivered");

            migrationBuilder.AddPrimaryKey(
                name: "PK__OutboxMessages",
                schema: "googleimport",
                table: "_OutboxMessages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__InternalCommands",
                schema: "googleimport",
                table: "_InternalCommands",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__MT_OutboxState",
                schema: "googleimport",
                table: "_MT_OutboxState",
                column: "OutboxId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__MT_OutboxMessage",
                schema: "googleimport",
                table: "_MT_OutboxMessage",
                column: "SequenceNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK__MT_InboxState_MessageId_ConsumerId",
                schema: "googleimport",
                table: "_MT_InboxState",
                columns: new[] { "MessageId", "ConsumerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__MT_InboxState",
                schema: "googleimport",
                table: "_MT_InboxState",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__OutboxMessages",
                schema: "googleimport",
                table: "_OutboxMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK__MT_OutboxState",
                schema: "googleimport",
                table: "_MT_OutboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK__MT_OutboxMessage",
                schema: "googleimport",
                table: "_MT_OutboxMessage");

            migrationBuilder.DropUniqueConstraint(
                name: "AK__MT_InboxState_MessageId_ConsumerId",
                schema: "googleimport",
                table: "_MT_InboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK__MT_InboxState",
                schema: "googleimport",
                table: "_MT_InboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK__InternalCommands",
                schema: "googleimport",
                table: "_InternalCommands");

            migrationBuilder.RenameTable(
                name: "_OutboxMessages",
                schema: "googleimport",
                newName: "OutboxMessages",
                newSchema: "googleimport");

            migrationBuilder.RenameTable(
                name: "_MT_OutboxState",
                schema: "googleimport",
                newName: "EBOutboxState",
                newSchema: "googleimport");

            migrationBuilder.RenameTable(
                name: "_MT_OutboxMessage",
                schema: "googleimport",
                newName: "EBOutboxMessage",
                newSchema: "googleimport");

            migrationBuilder.RenameTable(
                name: "_MT_InboxState",
                schema: "googleimport",
                newName: "EBInboxState",
                newSchema: "googleimport");

            migrationBuilder.RenameTable(
                name: "_InternalCommands",
                schema: "googleimport",
                newName: "InternalCommands",
                newSchema: "googleimport");

            migrationBuilder.RenameIndex(
                name: "IX__MT_OutboxState_Created",
                schema: "googleimport",
                table: "EBOutboxState",
                newName: "IX_EBOutboxState_Created");

            migrationBuilder.RenameIndex(
                name: "IX__MT_OutboxMessage_OutboxId_SequenceNumber",
                schema: "googleimport",
                table: "EBOutboxMessage",
                newName: "IX_EBOutboxMessage_OutboxId_SequenceNumber");

            migrationBuilder.RenameIndex(
                name: "IX__MT_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNu~",
                schema: "googleimport",
                table: "EBOutboxMessage",
                newName: "IX_EBOutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumb~");

            migrationBuilder.RenameIndex(
                name: "IX__MT_OutboxMessage_ExpirationTime",
                schema: "googleimport",
                table: "EBOutboxMessage",
                newName: "IX_EBOutboxMessage_ExpirationTime");

            migrationBuilder.RenameIndex(
                name: "IX__MT_OutboxMessage_EnqueueTime",
                schema: "googleimport",
                table: "EBOutboxMessage",
                newName: "IX_EBOutboxMessage_EnqueueTime");

            migrationBuilder.RenameIndex(
                name: "IX__MT_InboxState_Delivered",
                schema: "googleimport",
                table: "EBInboxState",
                newName: "IX_EBInboxState_Delivered");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutboxMessages",
                schema: "googleimport",
                table: "OutboxMessages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EBOutboxState",
                schema: "googleimport",
                table: "EBOutboxState",
                column: "OutboxId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EBOutboxMessage",
                schema: "googleimport",
                table: "EBOutboxMessage",
                column: "SequenceNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EBInboxState_MessageId_ConsumerId",
                schema: "googleimport",
                table: "EBInboxState",
                columns: new[] { "MessageId", "ConsumerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EBInboxState",
                schema: "googleimport",
                table: "EBInboxState",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternalCommands",
                schema: "googleimport",
                table: "InternalCommands",
                column: "Id");
        }
    }
}
