using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingItemsToSaleTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RandomNumber",
                table: "SaleProduct",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RandomNumber",
                table: "Sale",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "SMALLINT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandomNumber",
                table: "SaleProduct");

            migrationBuilder.AlterColumn<short>(
                name: "RandomNumber",
                table: "Sale",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");
        }
    }
}
