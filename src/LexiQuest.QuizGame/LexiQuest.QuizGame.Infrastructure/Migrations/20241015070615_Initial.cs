using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LexiQuest.QuizGame.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "quizgame");

            migrationBuilder.CreateTable(
                name: "_InternalCommands",
                schema: "quizgame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__InternalCommands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_MT_InboxState",
                schema: "quizgame",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uuid", nullable: false),
                    LockId = table.Column<Guid>(type: "uuid", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    Received = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReceiveCount = table.Column<int>(type: "integer", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Consumed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Delivered = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastSequenceNumber = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MT_InboxState", x => x.Id);
                    table.UniqueConstraint("AK__MT_InboxState_MessageId_ConsumerId", x => new { x.MessageId, x.ConsumerId });
                });

            migrationBuilder.CreateTable(
                name: "_MT_OutboxMessage",
                schema: "quizgame",
                columns: table => new
                {
                    SequenceNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnqueueTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SentTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Headers = table.Column<string>(type: "text", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    InboxMessageId = table.Column<Guid>(type: "uuid", nullable: true),
                    InboxConsumerId = table.Column<Guid>(type: "uuid", nullable: true),
                    OutboxId = table.Column<Guid>(type: "uuid", nullable: true),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    MessageType = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: true),
                    InitiatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: true),
                    SourceAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    DestinationAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ResponseAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    FaultAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ExpirationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MT_OutboxMessage", x => x.SequenceNumber);
                });

            migrationBuilder.CreateTable(
                name: "_MT_OutboxState",
                schema: "quizgame",
                columns: table => new
                {
                    OutboxId = table.Column<Guid>(type: "uuid", nullable: false),
                    LockId = table.Column<Guid>(type: "uuid", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Delivered = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastSequenceNumber = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MT_OutboxState", x => x.OutboxId);
                });

            migrationBuilder.CreateTable(
                name: "_OutboxMessages",
                schema: "quizgame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OccurredOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardDecks",
                schema: "quizgame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDecks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FaceUpCards",
                schema: "quizgame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Hint = table.Column<string>(type: "text", nullable: true),
                    Mistaken = table.Column<bool>(type: "boolean", nullable: false),
                    CompletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    PuzzleInfo_FaceDownCardId = table.Column<Guid>(type: "uuid", nullable: false),
                    ForeignWord = table.Column<string>(type: "text", nullable: false),
                    PartsOfSpeech = table.Column<string>(type: "text", nullable: false),
                    Transcription = table.Column<string>(type: "text", nullable: true),
                    From = table.Column<string>(type: "text", nullable: true),
                    Language = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: true),
                    Definitions = table.Column<List<string>>(type: "text[]", nullable: false),
                    Examples = table.Column<List<string>>(type: "text[]", nullable: false),
                    Synonims = table.Column<List<string>>(type: "text[]", nullable: false),
                    LastResult = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaceUpCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameStates",
                schema: "quizgame",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<string>(type: "text", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CardDeckId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStates", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                schema: "quizgame",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Games = table.Column<Guid[]>(type: "uuid[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StartNewGameSagaState",
                schema: "quizgame",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentState = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    StartNewGameId = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckLimitRequestId = table.Column<Guid>(type: "uuid", nullable: true),
                    PuzzleRequestId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlayerId = table.Column<string>(type: "text", nullable: true),
                    NewGameId = table.Column<Guid>(type: "uuid", nullable: true),
                    Initialized = table.Column<bool>(type: "boolean", nullable: false),
                    PuzzlesFetched = table.Column<bool>(type: "boolean", nullable: false),
                    GameCreated = table.Column<bool>(type: "boolean", nullable: false),
                    GameStarted = table.Column<bool>(type: "boolean", nullable: false),
                    RefusedAlreadyStarted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartNewGameSagaState", x => x.CorrelationId);
                });

            migrationBuilder.CreateTable(
                name: "FaceDownCards",
                schema: "quizgame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ForeignWord = table.Column<string>(type: "text", nullable: false),
                    PartsOfSpeech = table.Column<string>(type: "text", nullable: false),
                    Transcription = table.Column<string>(type: "text", nullable: true),
                    From = table.Column<string>(type: "text", nullable: true),
                    Language = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: true),
                    Definitions = table.Column<List<string>>(type: "text[]", nullable: false),
                    Examples = table.Column<List<string>>(type: "text[]", nullable: false),
                    Synonims = table.Column<List<string>>(type: "text[]", nullable: false),
                    DeckId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaceDownCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaceDownCards_CardDecks_DeckId",
                        column: x => x.DeckId,
                        principalSchema: "quizgame",
                        principalTable: "CardDecks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX__MT_InboxState_Delivered",
                schema: "quizgame",
                table: "_MT_InboxState",
                column: "Delivered");

            migrationBuilder.CreateIndex(
                name: "IX__MT_OutboxMessage_EnqueueTime",
                schema: "quizgame",
                table: "_MT_OutboxMessage",
                column: "EnqueueTime");

            migrationBuilder.CreateIndex(
                name: "IX__MT_OutboxMessage_ExpirationTime",
                schema: "quizgame",
                table: "_MT_OutboxMessage",
                column: "ExpirationTime");

            migrationBuilder.CreateIndex(
                name: "IX__MT_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNu~",
                schema: "quizgame",
                table: "_MT_OutboxMessage",
                columns: new[] { "InboxMessageId", "InboxConsumerId", "SequenceNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__MT_OutboxMessage_OutboxId_SequenceNumber",
                schema: "quizgame",
                table: "_MT_OutboxMessage",
                columns: new[] { "OutboxId", "SequenceNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__MT_OutboxState_Created",
                schema: "quizgame",
                table: "_MT_OutboxState",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_FaceDownCards_DeckId",
                schema: "quizgame",
                table: "FaceDownCards",
                column: "DeckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_InternalCommands",
                schema: "quizgame");

            migrationBuilder.DropTable(
                name: "_MT_InboxState",
                schema: "quizgame");

            migrationBuilder.DropTable(
                name: "_MT_OutboxMessage",
                schema: "quizgame");

            migrationBuilder.DropTable(
                name: "_MT_OutboxState",
                schema: "quizgame");

            migrationBuilder.DropTable(
                name: "_OutboxMessages",
                schema: "quizgame");

            migrationBuilder.DropTable(
                name: "FaceDownCards",
                schema: "quizgame");

            migrationBuilder.DropTable(
                name: "FaceUpCards",
                schema: "quizgame");

            migrationBuilder.DropTable(
                name: "GameStates",
                schema: "quizgame");

            migrationBuilder.DropTable(
                name: "Player",
                schema: "quizgame");

            migrationBuilder.DropTable(
                name: "StartNewGameSagaState",
                schema: "quizgame");

            migrationBuilder.DropTable(
                name: "CardDecks",
                schema: "quizgame");
        }
    }
}
