using Microsoft.EntityFrameworkCore.Migrations;

namespace LectureSystem.Migrations
{
    public partial class ModifySomeModelsAddStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Teaches",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Fields",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "CourseScores",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Attendances",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Teaches");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CourseScores");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Attendances");
        }
    }
}
