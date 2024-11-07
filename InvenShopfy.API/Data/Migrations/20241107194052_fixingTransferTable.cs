using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class fixingTransferTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Transfer");

            migrationBuilder.RenameColumn(
                name: "ProdudtId",
                table: "Transfer",
                newName: "ProductId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WarehouseProduct",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_ProductId",
                table: "Transfer",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_Product_ProductId",
                table: "Transfer",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Product_ProductId",
                table: "Transfer");

            migrationBuilder.DropIndex(
                name: "IX_Transfer_ProductId",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WarehouseProduct");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Transfer",
                newName: "ProdudtId");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Transfer",
                type: "VARCHAR",
                maxLength: 80,
                nullable: false,
                defaultValue: "");
        }
    }
}
