using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverflowApi.Migrations
{
    public partial class UpdatedAnswersQuestionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpDownVoteQuestion",
                table: "QuestionPosts");

            migrationBuilder.DropColumn(
                name: "UpDownVoteAnswer",
                table: "AnswersPost");

            migrationBuilder.AddColumn<int>(
                name: "DownVoteQuestion",
                table: "QuestionPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpVoteQuestion",
                table: "QuestionPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DownVoteAnswer",
                table: "AnswersPost",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpVoteAnswer",
                table: "AnswersPost",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownVoteQuestion",
                table: "QuestionPosts");

            migrationBuilder.DropColumn(
                name: "UpVoteQuestion",
                table: "QuestionPosts");

            migrationBuilder.DropColumn(
                name: "DownVoteAnswer",
                table: "AnswersPost");

            migrationBuilder.DropColumn(
                name: "UpVoteAnswer",
                table: "AnswersPost");

            migrationBuilder.AddColumn<int>(
                name: "UpDownVoteQuestion",
                table: "QuestionPosts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpDownVoteAnswer",
                table: "AnswersPost",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
