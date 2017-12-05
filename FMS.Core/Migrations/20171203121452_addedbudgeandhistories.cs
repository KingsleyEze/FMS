using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FMS.Core.Migrations
{
    public partial class addedbudgeandhistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EconomicId = table.Column<Guid>(nullable: false),
                    FundId = table.Column<Guid>(nullable: false),
                    TransactionDate = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_LineItems_EconomicId",
                        column: x => x.EconomicId,
                        principalTable: "LineItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Budgets_BankAccounts_FundId",
                        column: x => x.FundId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BudgetAmendHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    BudgetId = table.Column<Guid>(nullable: true),
                    TransactionDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetAmendHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetAmendHistories_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetAmendHistories_BudgetId",
                table: "BudgetAmendHistories",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_EconomicId",
                table: "Budgets",
                column: "EconomicId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_FundId",
                table: "Budgets",
                column: "FundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetAmendHistories");

            migrationBuilder.DropTable(
                name: "Budgets");
        }
    }
}
