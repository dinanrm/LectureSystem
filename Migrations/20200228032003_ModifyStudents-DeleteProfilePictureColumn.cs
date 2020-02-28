using Microsoft.EntityFrameworkCore.Migrations;

namespace LectureSystem.Migrations
{
    public partial class ModifyStudentsDeleteProfilePictureColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Students",
                type: "text",
                nullable: true);
        }
    }
}
