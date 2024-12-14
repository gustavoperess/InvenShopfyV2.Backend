using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class updatingPurchaseHandler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasProductBeenReturnn",
                table: "Purchase");

            migrationBuilder.AddColumn<bool>(
                name: "HasProductBeenReturned",
                table: "PurchaseProduct",
                type: "BOOLEAN",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasProductBeenReturned",
                table: "PurchaseProduct");

            migrationBuilder.AddColumn<bool>(
                name: "HasProductBeenReturnn",
                table: "Purchase",
                type: "BOOLEAN",
                nullable: false,
                defaultValue: false);
        }
    }
}
