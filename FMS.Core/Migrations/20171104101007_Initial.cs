using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FMS.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillPayables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Economic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fund = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeoCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPayables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillReceivable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Economic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fund = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeoCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillReceivable", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillPayables");

            migrationBuilder.DropTable(
                name: "BillReceivable");
        }
    }
}
