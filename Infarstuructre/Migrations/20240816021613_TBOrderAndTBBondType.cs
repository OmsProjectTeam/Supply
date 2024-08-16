using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class TBOrderAndTBBondType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBBondTypes",
                columns: table => new
                {
                    IdBondType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BondType = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBBondTypes", x => x.IdBondType);
                });

            migrationBuilder.CreateTable(
                name: "TBOrders",
                columns: table => new
                {
                    IdPurchaseOrder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBondType = table.Column<int>(type: "int", nullable: false),
                    IdMerchants = table.Column<int>(type: "int", nullable: false),
                    IdProductCategory = table.Column<int>(type: "int", nullable: false),
                    IdTypesProduct = table.Column<int>(type: "int", nullable: false),
                    IdProductInformation = table.Column<int>(type: "int", nullable: false),
                    IdBWareHouse = table.Column<int>(type: "int", nullable: false),
                    IdBWareHouseBranch = table.Column<int>(type: "int", nullable: false),
                    PurchaseAuotNoumber = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderNoumber = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    sellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GlobalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SpecialSalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    QuantityIn = table.Column<int>(type: "int", nullable: true),
                    QuantityOute = table.Column<int>(type: "int", nullable: true),
                    Qrcode = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBOrders", x => x.IdPurchaseOrder);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBBondTypes");

            migrationBuilder.DropTable(
                name: "TBOrders");
        }
    }
}
