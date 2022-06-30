using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningDiary.Migrations
{
    public partial class MigrationTableRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Task",
                schema: "dbo",
                newName: "LearningDiaryTask",
                newSchema: "dbo");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "LearningDiaryTask",
                schema: "dbo",
                newName: "Task",
                newSchema: "dbo");
        }
    }
}
