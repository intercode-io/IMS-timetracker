using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS_Timetracker.Migrations
{
    public partial class TimeLog_entity_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hours",
                table: "TimeLogs");

            migrationBuilder.DropColumn(
                name: "TimeEnd",
                table: "TimeLogs");

            migrationBuilder.RenameColumn(
                name: "TimeStart",
                table: "TimeLogs",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "Logs",
                table: "TimeLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logs",
                table: "TimeLogs");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "TimeLogs",
                newName: "TimeStart");

            migrationBuilder.AddColumn<float>(
                name: "Hours",
                table: "TimeLogs",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeEnd",
                table: "TimeLogs",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
