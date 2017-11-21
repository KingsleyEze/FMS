using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FMS.Core.Migrations
{
    public partial class AddEconomicAndFundToJournalListItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Economic",
                table: "JournalLineItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fund",
                table: "JournalLineItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Economic",
                table: "JournalLineItem");

            migrationBuilder.DropColumn(
                name: "Fund",
                table: "JournalLineItem");
        }
    }
}
