using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class updateTBs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TBTemplates");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "TBTemplates");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "TBNewsletters");

            migrationBuilder.DropColumn(
                name: "SendDate",
                table: "TBNewsletters");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TBSendLogs",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "NewsletterId",
                table: "TBSendLogs",
                newName: "IdNewsletter");

            migrationBuilder.AlterColumn<bool>(
                name: "CurrentState",
                table: "TBTemplates",
                type: "bit",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "((1))");

            migrationBuilder.AddColumn<bool>(
                name: "CurrentState",
                table: "TBSendLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "TBSendLogs");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "TBSendLogs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdNewsletter",
                table: "TBSendLogs",
                newName: "NewsletterId");

            migrationBuilder.AlterColumn<string>(
                name: "CurrentState",
                table: "TBTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((1))");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TBTemplates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "TBTemplates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "TBNewsletters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SendDate",
                table: "TBNewsletters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
