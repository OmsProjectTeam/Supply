using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class TBCompanyInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBCompanyInformations",
                columns: table => new
                {
                    IdCompanyInformation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmailCompany = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AddressEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShortDescriptionEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UrlFaceBook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlTwitter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlInstgram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlMap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCompanyInformations", x => x.IdCompanyInformation);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBCompanyInformations");
        }
    }
}
