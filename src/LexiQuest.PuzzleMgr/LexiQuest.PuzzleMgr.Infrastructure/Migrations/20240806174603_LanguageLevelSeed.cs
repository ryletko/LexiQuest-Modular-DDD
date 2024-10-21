using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LexiQuest.PuzzleMgr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LanguageLevelSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                columns: new[] { "Language", "TextRepresentation" },
                values: new object[,]
                {
                    { 0, "A1" },
                    { 0, "A2" },
                    { 0, "B1" },
                    { 0, "B2" },
                    { 0, "C1" },
                    { 1, "A1" },
                    { 1, "A1.1" },
                    { 1, "A1.2" },
                    { 1, "A2" },
                    { 1, "A2.1" },
                    { 1, "A2.2" },
                    { 1, "B1" },
                    { 1, "B1.1" },
                    { 1, "B1.2" },
                    { 1, "B2" },
                    { 1, "B2.1" },
                    { 1, "B2.2" },
                    { 1, "C1" },
                    { 1, "C1.1" },
                    { 1, "C1.2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 0, "A1" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 0, "A2" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 0, "B1" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 0, "B2" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 0, "C1" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "A1" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "A1.1" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "A1.2" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "A2" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "A2.1" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "A2.2" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "B1" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "B1.1" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "B1.2" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "B2" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "B2.1" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "B2.2" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "C1" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "C1.1" });

            migrationBuilder.DeleteData(
                schema: "puzzlemgr",
                table: "LanguageLevel",
                keyColumns: new[] { "Language", "TextRepresentation" },
                keyValues: new object[] { 1, "C1.2" });
        }
    }
}
