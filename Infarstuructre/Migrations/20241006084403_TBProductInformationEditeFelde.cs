using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class TBProductInformationEditeFelde : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "brand",
                table: "TBProductInformations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "storeSku",
                table: "TBProductInformations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "storeSoSku",
                table: "TBProductInformations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "brand",
                table: "TBProductInformations");

            migrationBuilder.DropColumn(
                name: "storeSku",
                table: "TBProductInformations");

            migrationBuilder.DropColumn(
                name: "storeSoSku",
                table: "TBProductInformations");
        }
    }
}
