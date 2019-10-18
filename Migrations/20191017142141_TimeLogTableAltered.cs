using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS_Timetracker.Migrations
{
    public partial class TimeLogTableAltered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeLogs_Projects_ProjectId",
                table: "TimeLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeLogs_Users_UserId",
                table: "TimeLogs");

            migrationBuilder.DropIndex(
                name: "IX_TimeLogs_ProjectId",
                table: "TimeLogs");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "TimeLogs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TimeLogs",
                newName: "ProjectUserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeLogs_UserId",
                table: "TimeLogs",
                newName: "IX_TimeLogs_ProjectUserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeLogs_ProjectsUsersRoles_ProjectUserRoleId",
                table: "TimeLogs",
                column: "ProjectUserRoleId",
                principalTable: "ProjectsUsersRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeLogs_ProjectsUsersRoles_ProjectUserRoleId",
                table: "TimeLogs");

            migrationBuilder.RenameColumn(
                name: "ProjectUserRoleId",
                table: "TimeLogs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeLogs_ProjectUserRoleId",
                table: "TimeLogs",
                newName: "IX_TimeLogs_UserId");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "TimeLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogs_ProjectId",
                table: "TimeLogs",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeLogs_Projects_ProjectId",
                table: "TimeLogs",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeLogs_Users_UserId",
                table: "TimeLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
