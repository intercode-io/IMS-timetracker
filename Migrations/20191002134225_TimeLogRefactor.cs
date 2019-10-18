using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS_Timetracker.Migrations
{
    public partial class TimeLogRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsersRoles_Roles_RoleId",
                table: "ProjectsUsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsersRoles_Users_UserId",
                table: "ProjectsUsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Users_UserId",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_UserId",
                table: "UserDetails");

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStart",
                table: "TimeLogs",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeEnd",
                table: "TimeLogs",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ProjectsUsersRoles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "ProjectsUsersRoles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsersRoles_Roles_RoleId",
                table: "ProjectsUsersRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsersRoles_Users_UserId",
                table: "ProjectsUsersRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsersRoles_Roles_RoleId",
                table: "ProjectsUsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsersRoles_Users_UserId",
                table: "ProjectsUsersRoles");

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

            migrationBuilder.AlterColumn<byte>(
                name: "TimeStart",
                table: "TimeLogs",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<byte>(
                name: "TimeEnd",
                table: "TimeLogs",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ProjectsUsersRoles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "ProjectsUsersRoles",
                nullable: true,
                oldClrType: typeof(int));

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

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_UserId",
                table: "UserDetails",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsersRoles_Roles_RoleId",
                table: "ProjectsUsersRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsersRoles_Users_UserId",
                table: "ProjectsUsersRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Users_UserId",
                table: "UserDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
