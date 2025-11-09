using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logix.Infrastructure.Migrations
{
    public partial class EditUsersToOverrideEmailMgt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Sys_User");

            migrationBuilder.AlterColumn<string>(
                name: "USER_EMAIL",
                table: "Sys_User",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "USER_EMAIL",
                table: "Sys_User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Sys_User",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
