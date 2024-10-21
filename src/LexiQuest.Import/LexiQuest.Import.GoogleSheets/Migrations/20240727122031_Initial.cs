using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexiQuest.Import.GoogleSheets.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "googleimport");

            migrationBuilder.CreateTable(
                name: "ImportSagaData",
                schema: "googleimport",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentState = table.Column<string>(type: "text", nullable: false),
                    ImportId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImporterId = table.Column<string>(type: "text", nullable: false),
                    ImportSourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Language = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Initialized = table.Column<bool>(type: "boolean", nullable: false),
                    Fetched = table.Column<bool>(type: "boolean", nullable: false),
                    SavedInPuzzleMgr = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportSagaData", x => x.CorrelationId);
                });

            migrationBuilder.CreateTable(
                name: "ImportSource",
                schema: "googleimport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImporterId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportSource", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportSagaData",
                schema: "googleimport");

            migrationBuilder.DropTable(
                name: "ImportSource",
                schema: "googleimport");
        }
    }
}
