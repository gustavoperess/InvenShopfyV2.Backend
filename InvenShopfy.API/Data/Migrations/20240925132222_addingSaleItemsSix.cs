using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingSaleItemsSix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "SaleProduct",
                newName: "TotalPricePerProduct");

            migrationBuilder.RenameColumn(
                name: "SingleQuantitySold",
                table: "SaleProduct",
                newName: "TotalQuantitySoldPerProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalQuantitySoldPerProduct",
                table: "SaleProduct",
                newName: "SingleQuantitySold");

            migrationBuilder.RenameColumn(
                name: "TotalPricePerProduct",
                table: "SaleProduct",
                newName: "TotalPrice");
        }
    }
}
