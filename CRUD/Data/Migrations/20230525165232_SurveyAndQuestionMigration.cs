using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD.Data.Migrations
{
    /// <inheritdoc />
    public partial class SurveyAndQuestionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    SurveyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubmitTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.SurveyId);
                });

            migrationBuilder.CreateTable(
                name: "SurveyAnswers",
                columns: table => new
                {
                    SurveyAnswerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Answer = table.Column<string>(type: "TEXT", maxLength: 100000, nullable: false),
                    SurveyId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswers", x => x.SurveyAnswerId);
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_QuestionId",
                table: "SurveyAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_SurveyId",
                table: "SurveyAnswers",
                column: "SurveyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyAnswers");

            migrationBuilder.DropTable(
                name: "Surveys");
        }
    }
}
