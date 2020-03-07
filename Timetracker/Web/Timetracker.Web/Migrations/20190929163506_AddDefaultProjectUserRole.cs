using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS_Timetracker.Migrations
{
    public partial class AddDefaultProjectUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProjectsUsersRoles",
                columns: new[] { "Id", "ProjectId", "RoleId", "UserId" },
                values: new object[] { 6, 1, 1, 2 });

            migrationBuilder.InsertData(
                table: "ProjectsUsersRoles",
                columns: new[] { "Id", "ProjectId", "RoleId", "UserId" },
                values: new object[] { 7, 2, 1, 2 });

            migrationBuilder.InsertData(
                table: "ProjectsUsersRoles",
                columns: new[] { "Id", "ProjectId", "RoleId", "UserId" },
                values: new object[] { 8, 3, 1, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectsUsersRoles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProjectsUsersRoles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProjectsUsersRoles",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
