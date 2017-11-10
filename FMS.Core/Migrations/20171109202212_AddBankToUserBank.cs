using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FMS.Core.Migrations
{
    public partial class AddBankToUserBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankBranchCode",
                table: "AppUserBanks");

            migrationBuilder.AddColumn<Guid>(
                name: "BankId",
                table: "AppUserBanks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserBanks_BankId",
                table: "AppUserBanks",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserBanks_Banks_BankId",
                table: "AppUserBanks",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserBanks_Banks_BankId",
                table: "AppUserBanks");

            migrationBuilder.DropIndex(
                name: "IX_AppUserBanks_BankId",
                table: "AppUserBanks");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "AppUserBanks");

            migrationBuilder.AddColumn<string>(
                name: "BankBranchCode",
                table: "AppUserBanks",
                nullable: true);
        }
    }
}
