using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chatter.DAL.Migrations
{
    public partial class really : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignInLogs_Users_UserId",
                table: "SignInLogs");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "SignInLogs",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SignInLogs_Users_UserId",
                table: "SignInLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignInLogs_Users_UserId",
                table: "SignInLogs");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "SignInLogs",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_SignInLogs_Users_UserId",
                table: "SignInLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
