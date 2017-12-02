using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FMS.Core.Migrations
{
    public partial class AddedAccountGroupType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AccountSubTypeId",
                table: "LineItems",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountGroupType",
                table: "LineItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountGroupType",
                table: "LineItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountSubTypeId",
                table: "LineItems",
                nullable: true,
                oldClrType: typeof(Guid));
        }
    }
}
