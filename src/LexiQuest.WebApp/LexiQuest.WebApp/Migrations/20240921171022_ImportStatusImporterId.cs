using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexiQuest.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class ImportStatusImporterId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImporterId",
                schema: "webapp",
                table: "ImportStatus",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImporterId",
                schema: "webapp",
                table: "ImportStatus");
        }
    }
}
