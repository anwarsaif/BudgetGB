using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logix.Infrastructure.Migrations
{
    public partial class addUrlColumnToSysScreenTableMgt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Sys_Screen",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "Sys_Screen");
        }
    }
}
