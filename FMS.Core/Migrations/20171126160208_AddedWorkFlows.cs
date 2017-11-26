using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FMS.Core.Migrations
{
    public partial class AddedWorkFlows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PayeeId",
                table: "BillReceivable",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Fund",
                table: "BillReceivable",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Economic",
                table: "BillReceivable",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BillReceivable",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BillReceivable",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BillPayables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PayableWorkFlows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BillPayableId = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayableWorkFlows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayableWorkFlows_BillPayables_BillPayableId",
                        column: x => x.BillPayableId,
                        principalTable: "BillPayables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceivableWorkFlows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BillReceivableId = table.Column<Guid>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivableWorkFlows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceivableWorkFlows_BillReceivable_BillReceivableId",
                        column: x => x.BillReceivableId,
                        principalTable: "BillReceivable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayableWorkFlows_BillPayableId",
                table: "PayableWorkFlows",
                column: "BillPayableId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableWorkFlows_BillReceivableId",
                table: "ReceivableWorkFlows",
                column: "BillReceivableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayableWorkFlows");

            migrationBuilder.DropTable(
                name: "ReceivableWorkFlows");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BillReceivable");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BillPayables");

            migrationBuilder.AlterColumn<string>(
                name: "PayeeId",
                table: "BillReceivable",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fund",
                table: "BillReceivable",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Economic",
                table: "BillReceivable",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BillReceivable",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
