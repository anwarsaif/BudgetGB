using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logix.Infrastructure.Migrations
{
    public partial class testSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainListDtos",
                columns: table => new
                {
                    Icon_Css = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color_Css = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SCREEN_ID = table.Column<int>(type: "int", nullable: false),
                    SCREEN_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SCREEN_NAME2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISDEL = table.Column<bool>(type: "bit", nullable: false),
                    System_Id = table.Column<int>(type: "int", nullable: false),
                    Parent_Id = table.Column<int>(type: "int", nullable: false),
                    Sort_no = table.Column<int>(type: "int", nullable: false),
                    SCREEN_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRIVE_ID = table.Column<int>(type: "int", nullable: false),
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    Expr1 = table.Column<int>(type: "int", nullable: false),
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
                name: "MainListDtos");

        }
    }
}
