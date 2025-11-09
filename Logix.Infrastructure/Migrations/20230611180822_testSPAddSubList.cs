using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logix.Infrastructure.Migrations
{
    public partial class testSPAddSubList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "USER_ID",
                table: "MainListDtos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "SCREEN_ID",
                table: "MainListDtos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "PRIVE_ID",
                table: "MainListDtos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Expr1",
                table: "MainListDtos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "SubListDtos",
                columns: table => new
                {
                    Icon_Css = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SCREEN_ID = table.Column<long>(type: "bigint", nullable: false),
                    SCREEN_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SCREEN_NAME2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISDEL = table.Column<bool>(type: "bit", nullable: false),
                    System_Id = table.Column<int>(type: "int", nullable: false),
                    Parent_Id = table.Column<int>(type: "int", nullable: true),
                    Sort_no = table.Column<int>(type: "int", nullable: false),
                    SCREEN_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRIVE_ID = table.Column<long>(type: "bigint", nullable: false),
                    USER_ID = table.Column<long>(type: "bigint", nullable: true),
                    Expr1 = table.Column<long>(type: "bigint", nullable: false),
                    SCREEN_SHOW = table.Column<bool>(type: "bit", nullable: false),
                    SCREEN_ADD = table.Column<bool>(type: "bit", nullable: false),
                    SCREEN_EDIT = table.Column<bool>(type: "bit", nullable: false),
                    SCREEN_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    SCREEN_PRINT = table.Column<bool>(type: "bit", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    Cnt = table.Column<int>(type: "int", nullable: false),
                    Folder = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubListDtos");

            migrationBuilder.AlterColumn<int>(
                name: "USER_ID",
                table: "MainListDtos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SCREEN_ID",
                table: "MainListDtos",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "PRIVE_ID",
                table: "MainListDtos",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Expr1",
                table: "MainListDtos",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
