using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class refactoringNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Unit",
                newName: "UnitShortName");

            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Unit",
                newName: "UnitName");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "SalesReturn",
                newName: "ReturnTotalAmount");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Product",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Product",
                newName: "ProductPrice");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Notification",
                newName: "NotificationTitle");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Message",
                newName: "MessageTitle");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customer",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Brand",
                newName: "BrandName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitShortName",
                table: "Unit",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "UnitName",
                table: "Unit",
                newName: "ShortName");

            migrationBuilder.RenameColumn(
                name: "ReturnTotalAmount",
                table: "SalesReturn",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "ProductPrice",
                table: "Product",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Product",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "NotificationTitle",
                table: "Notification",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "MessageTitle",
                table: "Message",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Customer",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BrandName",
                table: "Brand",
                newName: "Title");
        }
    }
}
