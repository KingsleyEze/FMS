using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FMS.Core.Migrations
{
    public partial class AddJournalAndJournalLineItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Journal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Economic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fund = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JournalLineItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    JournalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalLineItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalLineItem_Journal_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalLineItem_JournalId",
                table: "JournalLineItem",
                column: "JournalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalLineItem");

            migrationBuilder.DropTable(
                name: "Journal");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Staffs");
        }
    }
}
