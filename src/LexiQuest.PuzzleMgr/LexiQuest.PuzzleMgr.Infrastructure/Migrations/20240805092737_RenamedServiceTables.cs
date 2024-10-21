using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexiQuest.PuzzleMgr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamedServiceTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OutboxMessages",
                schema: "puzzlemgr",
                table: "OutboxMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InternalCommands",
                schema: "puzzlemgr",
                table: "InternalCommands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EBOutboxState",
                schema: "puzzlemgr",
                table: "EBOutboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EBOutboxMessage",
                schema: "puzzlemgr",
                table: "EBOutboxMessage");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_EBInboxState_MessageId_ConsumerId",
                schema: "puzzlemgr",
                table: "EBInboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EBInboxState",
                schema: "puzzlemgr",
                table: "EBInboxState");

            migrationBuilder.RenameTable(
                name: "OutboxMessages",
                schema: "puzzlemgr",
                newName: "_OutboxMessages",
                newSchema: "puzzlemgr");

            migrationBuilder.RenameTable(
                name: "InternalCommands",
                schema: "puzzlemgr",
                newName: "_InternalCommands",
                newSchema: "puzzlemgr");

            migrationBuilder.RenameTable(
                name: "EBOutboxState",
                schema: "puzzlemgr",
                newName: "_MT_OutboxState",
                newSchema: "puzzlemgr");

            migrationBuilder.RenameTable(
                name: "EBOutboxMessage",
                schema: "puzzlemgr",
                newName: "_MT_OutboxMessage",
                newSchema: "puzzlemgr");

            migrationBuilder.RenameTable(
                name: "EBInboxState",
                schema: "puzzlemgr",
                newName: "_MT_InboxState",
                newSchema: "puzzlemgr");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxState_Created",
                schema: "puzzlemgr",
                table: "_MT_OutboxState",
                newName: "IX__MT_OutboxState_Created");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxMessage_OutboxId_SequenceNumber",
                schema: "puzzlemgr",
                table: "_MT_OutboxMessage",
                newName: "IX__MT_OutboxMessage_OutboxId_SequenceNumber");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumb~",
                schema: "puzzlemgr",
                table: "_MT_OutboxMessage",
                newName: "IX__MT_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNu~");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxMessage_ExpirationTime",
                schema: "puzzlemgr",
                table: "_MT_OutboxMessage",
                newName: "IX__MT_OutboxMessage_ExpirationTime");

            migrationBuilder.RenameIndex(
                name: "IX_EBOutboxMessage_EnqueueTime",
                schema: "puzzlemgr",
                table: "_MT_OutboxMessage",
                newName: "IX__MT_OutboxMessage_EnqueueTime");

            migrationBuilder.RenameIndex(
                name: "IX_EBInboxState_Delivered",
                schema: "puzzlemgr",
                table: "_MT_InboxState",
                newName: "IX__MT_InboxState_Delivered");

            migrationBuilder.AddPrimaryKey(
                name: "PK__OutboxMessages",
                schema: "puzzlemgr",
                table: "_OutboxMessages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__InternalCommands",
                schema: "puzzlemgr",
                table: "_InternalCommands",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__MT_OutboxState",
                schema: "puzzlemgr",
                table: "_MT_OutboxState",
                column: "OutboxId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__MT_OutboxMessage",
                schema: "puzzlemgr",
                table: "_MT_OutboxMessage",
                column: "SequenceNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK__MT_InboxState_MessageId_ConsumerId",
                schema: "puzzlemgr",
                table: "_MT_InboxState",
                columns: new[] { "MessageId", "ConsumerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__MT_InboxState",
                schema: "puzzlemgr",
                table: "_MT_InboxState",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__OutboxMessages",
                schema: "puzzlemgr",
                table: "_OutboxMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK__MT_OutboxState",
                schema: "puzzlemgr",
                table: "_MT_OutboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK__MT_OutboxMessage",
                schema: "puzzlemgr",
                table: "_MT_OutboxMessage");

            migrationBuilder.DropUniqueConstraint(
                name: "AK__MT_InboxState_MessageId_ConsumerId",
                schema: "puzzlemgr",
                table: "_MT_InboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK__MT_InboxState",
                schema: "puzzlemgr",
                table: "_MT_InboxState");

            migrationBuilder.DropPrimaryKey(
                name: "PK__InternalCommands",
                schema: "puzzlemgr",
                table: "_InternalCommands");

            migrationBuilder.RenameTable(
                name: "_OutboxMessages",
                schema: "puzzlemgr",
                newName: "OutboxMessages",
                newSchema: "puzzlemgr");

            migrationBuilder.RenameTable(
                name: "_MT_OutboxState",
                schema: "puzzlemgr",
                newName: "EBOutboxState",
                newSchema: "puzzlemgr");

            migrationBuilder.RenameTable(
                name: "_MT_OutboxMessage",
                schema: "puzzlemgr",
                newName: "EBOutboxMessage",
                newSchema: "puzzlemgr");

            migrationBuilder.RenameTable(
                name: "_MT_InboxState",
                schema: "puzzlemgr",
                newName: "EBInboxState",
                newSchema: "puzzlemgr");

            migrationBuilder.RenameTable(
                name: "_InternalCommands",
                schema: "puzzlemgr",
                newName: "InternalCommands",
                newSchema: "puzzlemgr");

            migrationBuilder.RenameIndex(
                name: "IX__MT_OutboxState_Created",
                schema: "puzzlemgr",
                table: "EBOutboxState",
                newName: "IX_EBOutboxState_Created");

            migrationBuilder.RenameIndex(
                name: "IX__MT_OutboxMessage_OutboxId_SequenceNumber",
                schema: "puzzlemgr",
                table: "EBOutboxMessage",
                newName: "IX_EBOutboxMessage_OutboxId_SequenceNumber");

            migrationBuilder.RenameIndex(
                name: "IX__MT_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNu~",
                schema: "puzzlemgr",
                table: "EBOutboxMessage",
                newName: "IX_EBOutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumb~");

            migrationBuilder.RenameIndex(
                name: "IX__MT_OutboxMessage_ExpirationTime",
                schema: "puzzlemgr",
                table: "EBOutboxMessage",
                newName: "IX_EBOutboxMessage_ExpirationTime");

            migrationBuilder.RenameIndex(
                name: "IX__MT_OutboxMessage_EnqueueTime",
                schema: "puzzlemgr",
                table: "EBOutboxMessage",
                newName: "IX_EBOutboxMessage_EnqueueTime");

            migrationBuilder.RenameIndex(
                name: "IX__MT_InboxState_Delivered",
                schema: "puzzlemgr",
                table: "EBInboxState",
                newName: "IX_EBInboxState_Delivered");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutboxMessages",
                schema: "puzzlemgr",
                table: "OutboxMessages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EBOutboxState",
                schema: "puzzlemgr",
                table: "EBOutboxState",
                column: "OutboxId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EBOutboxMessage",
                schema: "puzzlemgr",
                table: "EBOutboxMessage",
                column: "SequenceNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EBInboxState_MessageId_ConsumerId",
                schema: "puzzlemgr",
                table: "EBInboxState",
                columns: new[] { "MessageId", "ConsumerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EBInboxState",
                schema: "puzzlemgr",
                table: "EBInboxState",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternalCommands",
                schema: "puzzlemgr",
                table: "InternalCommands",
                column: "Id");
        }
    }
}
