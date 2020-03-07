using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS_Timetracker.Migrations
{
    public partial class duration_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "TimeLogs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "TimeLogs");
        }
    }
}
