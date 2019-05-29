using Microsoft.EntityFrameworkCore.Migrations;

namespace Chatter.DAL.Migrations
{
    public partial class User_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SingInLogs_Users_UserId",
                table: "SingInLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SingInLogs",
                table: "SingInLogs");

            migrationBuilder.RenameTable(
                name: "SingInLogs",
                newName: "SignInLogs");

            migrationBuilder.RenameIndex(
                name: "IX_SingInLogs_UserId",
                table: "SignInLogs",
                newName: "IX_SignInLogs_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SignInLogs",
                table: "SignInLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SignInLogs_Users_UserId",
                table: "SignInLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignInLogs_Users_UserId",
                table: "SignInLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SignInLogs",
                table: "SignInLogs");

            migrationBuilder.RenameTable(
                name: "SignInLogs",
                newName: "SingInLogs");

            migrationBuilder.RenameIndex(
                name: "IX_SignInLogs_UserId",
                table: "SingInLogs",
                newName: "IX_SingInLogs_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SingInLogs",
                table: "SingInLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SingInLogs_Users_UserId",
                table: "SingInLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
