using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class catTabelByRahan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBConnectAndDisConnects",
                columns: table => new
                {
                    IdConnectAndDisConnect = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeConnection = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBConnectAndDisConnects", x => x.IdConnectAndDisConnect);
                });

            migrationBuilder.CreateTable(
                name: "TBMessageChats",
                columns: table => new
                {
                    IdMessageChat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReciverId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgMsg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageeTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMessageChats", x => x.IdMessageChat);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBConnectAndDisConnects");

            migrationBuilder.DropTable(
                name: "TBMessageChats");
        }
    }
}
