using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StackOverflowApi.Migrations
{
    public partial class AddedQuestionAndAnswersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(nullable: true),
                    NumberOfViews = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    UpDownVoteQuestion = table.Column<int>(nullable: false),
                    DateOfPost = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnswersPost",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnswerContent = table.Column<string>(nullable: true),
                    UpDownVoteAnswer = table.Column<int>(nullable: false),
                    DateOfPost = table.Column<DateTime>(nullable: false),
                    QuestionPostId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswersPost_QuestionPosts_QuestionPostId",
                        column: x => x.QuestionPostId,
                        principalTable: "QuestionPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswersPost_QuestionPostId",
                table: "AnswersPost",
                column: "QuestionPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswersPost");

            migrationBuilder.DropTable(
                name: "QuestionPosts");
        }
    }
}
