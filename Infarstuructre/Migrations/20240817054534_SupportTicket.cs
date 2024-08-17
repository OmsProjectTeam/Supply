using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class SupportTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBSupportTickets",
                columns: table => new
                {
                    IdSupportTicket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSupportTicketType = table.Column<int>(type: "int", nullable: false),
                    IdSupportTicketStatus = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupportTicketNo = table.Column<int>(type: "int", nullable: false),
                    FollowUpMail = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TicketDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSupportTickets", x => x.IdSupportTicket);
                });

            migrationBuilder.CreateTable(
                name: "TBSupportTicketStatuss",
                columns: table => new
                {
                    IdSupportTicketStatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupportTicketStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSupportTicketStatuss", x => x.IdSupportTicketStatus);
                });

            migrationBuilder.CreateTable(
                name: "TBSupportTicketTypes",
                columns: table => new
                {
                    IdSupportTicketType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupportTicketType = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSupportTicketTypes", x => x.IdSupportTicketType);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBSupportTickets");

            migrationBuilder.DropTable(
                name: "TBSupportTicketStatuss");

            migrationBuilder.DropTable(
                name: "TBSupportTicketTypes");
        }
    }
}
