using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LectureSystem.Migrations
{
    public partial class ModifyClassSchedulesRemoveDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "ClassSchedules");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "ClassSchedules",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "ClassSchedules",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "ClassSchedules",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "ClassSchedules",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ClassSchedules",
                type: "date",
                nullable: true);
        }
    }
}
