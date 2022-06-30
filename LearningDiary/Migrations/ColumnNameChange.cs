using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningDiary.Migrations
{
    public partial class MigrationWithDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable("Tasks", "LearningDiaryTasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Tasks");

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "TopicId", "AlreadyStudied", "CompletionDate", "Description", "EstimatedTimeToMaster", "InProgress", "Source", "StartLearningDate", "TimeSpent", "Title" },
                values: new object[] { 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "testi", 10.0, false, "web", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "testi" });
        }
    }
}
