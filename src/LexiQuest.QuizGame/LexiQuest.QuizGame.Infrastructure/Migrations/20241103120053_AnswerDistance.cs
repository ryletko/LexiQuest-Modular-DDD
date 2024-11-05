using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexiQuest.QuizGame.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AnswerDistance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AnswerDistance",
                schema: "quizgame",
                table: "FaceUpCards",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerDistance",
                schema: "quizgame",
                table: "FaceUpCards");
        }
    }
}
