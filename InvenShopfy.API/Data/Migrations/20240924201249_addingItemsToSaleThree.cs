using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingItemsToSaleThree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RandomNumber",
                table: "SaleProduct",
                newName: "ReferenceNumber");

            migrationBuilder.RenameColumn(
                name: "RandomNumber",
                table: "Sale",
                newName: "ReferenceNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferenceNumber",
                table: "SaleProduct",
                newName: "RandomNumber");

            migrationBuilder.RenameColumn(
                name: "ReferenceNumber",
                table: "Sale",
                newName: "RandomNumber");
        }
    }
}
