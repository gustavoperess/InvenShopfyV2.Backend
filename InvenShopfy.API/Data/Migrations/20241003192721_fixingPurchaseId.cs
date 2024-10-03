using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class fixingPurchaseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseProduct",
                table: "PurchaseProduct");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseProduct_AddPurchaseId",
                table: "PurchaseProduct");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "PurchaseProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseProduct",
                table: "PurchaseProduct",
                columns: new[] { "AddPurchaseId", "ProductId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseProduct",
                table: "PurchaseProduct");

            migrationBuilder.AddColumn<long>(
                name: "PurchaseId",
                table: "PurchaseProduct",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseProduct",
                table: "PurchaseProduct",
                columns: new[] { "PurchaseId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProduct_AddPurchaseId",
                table: "PurchaseProduct",
                column: "AddPurchaseId");
        }
    }
}
