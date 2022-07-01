using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningDiary.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
               name: "Tasks",
               schema: "dbo",
               newName: "LearningDiaryTasks",
               newSchema: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                 name: "LearningDiaryTasks",
                 schema: "dbo",
                 newName: "Tasks",
                 newSchema: "dbo");
        }
    }
}
