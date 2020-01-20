using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS_Timetracker.Migrations
{
    public partial class Project_entity_color_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Projects");
        }
    }
}
