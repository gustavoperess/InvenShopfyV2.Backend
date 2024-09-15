using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingFieldsToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DifferPriceWarehouse",
                table: "Product",
                type: "BOOLEAN",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Expired",
                table: "Product",
                type: "BOOLEAN",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Featured",
                table: "Product",
                type: "BOOLEAN",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sale",
                table: "Product",
                type: "BOOLEAN",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DifferPriceWarehouse",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Expired",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Featured",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Sale",
                table: "Product");
        }
    }
}
