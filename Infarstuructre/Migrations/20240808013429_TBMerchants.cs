using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class TBMerchants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "TBWareHouses",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "TBMerchantss",
                columns: table => new
                {
                    IdMerchants = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MerchantPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MerchantEmaile = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    MerchantWeb = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    MerchantAddres = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    MerchantOnerName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    MerchantOnerPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MerchantOnerEmail = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMerchantss", x => x.IdMerchants);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBMerchantss");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "TBWareHouses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);
        }
    }
}
