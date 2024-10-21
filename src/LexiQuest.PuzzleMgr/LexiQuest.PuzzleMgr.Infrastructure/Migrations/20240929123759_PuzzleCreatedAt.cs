using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexiQuest.PuzzleMgr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PuzzleCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Puzzles_LanguageLevel_LevelLanguage_LevelTextRepresentation",
                schema: "puzzlemgr",
                table: "Puzzles");

            migrationBuilder.RenameColumn(
                name: "Created",
                schema: "puzzlemgr",
                table: "Puzzles",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                schema: "puzzlemgr",
                table: "PuzzleCollection",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Transcription",
                schema: "puzzlemgr",
                table: "Puzzles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LevelTextRepresentation",
                schema: "puzzlemgr",
                table: "Puzzles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "LevelLanguage",
                schema: "puzzlemgr",
                table: "Puzzles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "From",
                schema: "puzzlemgr",
                table: "Puzzles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Puzzles_LanguageLevel_LevelLanguage_LevelTextRepresentation",
                schema: "puzzlemgr",
                table: "Puzzles",
                columns: new[] { "LevelLanguage", "LevelTextRepresentation" },
                principalSchema: "puzzlemgr",
                principalTable: "LanguageLevel",
                principalColumns: new[] { "Language", "TextRepresentation" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Puzzles_LanguageLevel_LevelLanguage_LevelTextRepresentation",
                schema: "puzzlemgr",
                table: "Puzzles");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "puzzlemgr",
                table: "Puzzles",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "puzzlemgr",
                table: "PuzzleCollection",
                newName: "Created");

            migrationBuilder.AlterColumn<string>(
                name: "Transcription",
                schema: "puzzlemgr",
                table: "Puzzles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LevelTextRepresentation",
                schema: "puzzlemgr",
                table: "Puzzles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LevelLanguage",
                schema: "puzzlemgr",
                table: "Puzzles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "From",
                schema: "puzzlemgr",
                table: "Puzzles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Puzzles_LanguageLevel_LevelLanguage_LevelTextRepresentation",
                schema: "puzzlemgr",
                table: "Puzzles",
                columns: new[] { "LevelLanguage", "LevelTextRepresentation" },
                principalSchema: "puzzlemgr",
                principalTable: "LanguageLevel",
                principalColumns: new[] { "Language", "TextRepresentation" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
