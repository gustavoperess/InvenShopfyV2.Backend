using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class updatingSalesDateAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Sale",
                type: "TIMESTAMP",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMPTZ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Sale",
                type: "TIMESTAMPTZ",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP");
        }
    }
}
