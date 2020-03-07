using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS_Timetracker.Migrations
{
    public partial class DisablePrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsersRoles_Projects_ProjectId",
                table: "ProjectsUsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsersRoles_Roles_RoleId",
                table: "ProjectsUsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsersRoles_Users_UserId",
                table: "ProjectsUsersRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectsUsersRoles",
                table: "ProjectsUsersRoles");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "ProjectsUsersRoles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectsUsersRoles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ProjectsUsersRoles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProjectsUsersRoles",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectsUsersRoles",
                table: "ProjectsUsersRoles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsUsersRoles_UserId",
                table: "ProjectsUsersRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsersRoles_Projects_ProjectId",
                table: "ProjectsUsersRoles",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsersRoles_Projects_ProjectId",
                table: "ProjectsUsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsersRoles_Roles_RoleId",
                table: "ProjectsUsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsersRoles_Users_UserId",
                table: "ProjectsUsersRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectsUsersRoles",
                table: "ProjectsUsersRoles");

            migrationBuilder.DropIndex(
                name: "IX_ProjectsUsersRoles_UserId",
                table: "ProjectsUsersRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectsUsersRoles");

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

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectsUsersRoles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectsUsersRoles",
                table: "ProjectsUsersRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsersRoles_Projects_ProjectId",
                table: "ProjectsUsersRoles",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
