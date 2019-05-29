using Microsoft.EntityFrameworkCore.Migrations;

namespace Chatter.DAL.Migrations
{
    public partial class DBUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SingInLogTime",
                table: "SingInLogs",
                newName: "SignInLogTime");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Organizations",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Groups",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SignInLogTime",
                table: "SingInLogs",
                newName: "SingInLogTime");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Organizations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Groups",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
