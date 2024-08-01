using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class Faq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBFAQDescreptions",
                columns: table => new
                {
                    IdFAQDescreption = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFAQ = table.Column<int>(type: "int", nullable: false),
                    Descreption = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    DateEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBFAQDescreptions", x => x.IdFAQDescreption);
                });

            migrationBuilder.CreateTable(
                name: "TBFAQLists",
                columns: table => new
                {
                    IdFAQList = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFAQ = table.Column<int>(type: "int", nullable: false),
                    ListFAQ = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    DateEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBFAQLists", x => x.IdFAQList);
                });

            migrationBuilder.CreateTable(
                name: "TBFAQs",
                columns: table => new
                {
                    IdFAQ = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FAQ = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    DateEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBFAQs", x => x.IdFAQ);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBFAQDescreptions");

            migrationBuilder.DropTable(
                name: "TBFAQLists");

            migrationBuilder.DropTable(
                name: "TBFAQs");
        }
    }
}
