using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LexiQuest.PuzzleMgr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "puzzlemgr");

            migrationBuilder.CreateTable(
                name: "InternalCommands",
                schema: "puzzlemgr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalCommands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageLevel",
                schema: "puzzlemgr",
                columns: table => new
                {
                    Language = table.Column<int>(type: "integer", nullable: false),
                    TextRepresentation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageLevel", x => new { x.Language, x.TextRepresentation });
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "puzzlemgr",
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
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PuzzleCollection",
                schema: "puzzlemgr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Puzzles = table.Column<Guid[]>(type: "uuid[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuzzleCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Puzzles",
                schema: "puzzlemgr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PuzzleOwnerId = table.Column<string>(type: "text", nullable: false),
                    ForeignWordLanguage = table.Column<int>(type: "integer", nullable: false),
                    ForeignWordText = table.Column<string>(type: "text", nullable: false),
                    PartsOfSpeech = table.Column<int>(type: "integer", nullable: false),
                    Transcription = table.Column<string>(type: "text", nullable: false),
                    From = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<int>(type: "integer", nullable: false),
                    LevelLanguage = table.Column<int>(type: "integer", nullable: false),
                    LevelTextRepresentation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puzzles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Puzzles_LanguageLevel_LevelLanguage_LevelTextRepresentation",
                        columns: x => new { x.LevelLanguage, x.LevelTextRepresentation },
                        principalSchema: "puzzlemgr",
                        principalTable: "LanguageLevel",
                        principalColumns: new[] { "Language", "TextRepresentation" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Definitions",
                schema: "puzzlemgr",
                columns: table => new
                {
                    PuzzleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Definitions", x => new { x.PuzzleId, x.Id });
                    table.ForeignKey(
                        name: "FK_Definitions_Puzzles_PuzzleId",
                        column: x => x.PuzzleId,
                        principalSchema: "puzzlemgr",
                        principalTable: "Puzzles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examples",
                schema: "puzzlemgr",
                columns: table => new
                {
                    PuzzleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => new { x.PuzzleId, x.Id });
                    table.ForeignKey(
                        name: "FK_Examples_Puzzles_PuzzleId",
                        column: x => x.PuzzleId,
                        principalSchema: "puzzlemgr",
                        principalTable: "Puzzles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Synonims",
                schema: "puzzlemgr",
                columns: table => new
                {
                    PuzzleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Synonims", x => new { x.PuzzleId, x.Id });
                    table.ForeignKey(
                        name: "FK_Synonims_Puzzles_PuzzleId",
                        column: x => x.PuzzleId,
                        principalSchema: "puzzlemgr",
                        principalTable: "Puzzles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Puzzles_LevelLanguage_LevelTextRepresentation",
                schema: "puzzlemgr",
                table: "Puzzles",
                columns: new[] { "LevelLanguage", "LevelTextRepresentation" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Definitions",
                schema: "puzzlemgr");

            migrationBuilder.DropTable(
                name: "Examples",
                schema: "puzzlemgr");

            migrationBuilder.DropTable(
                name: "InternalCommands",
                schema: "puzzlemgr");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "puzzlemgr");

            migrationBuilder.DropTable(
                name: "PuzzleCollection",
                schema: "puzzlemgr");

            migrationBuilder.DropTable(
                name: "Synonims",
                schema: "puzzlemgr");

            migrationBuilder.DropTable(
                name: "Puzzles",
                schema: "puzzlemgr");

            migrationBuilder.DropTable(
                name: "LanguageLevel",
                schema: "puzzlemgr");
        }
    }
}
