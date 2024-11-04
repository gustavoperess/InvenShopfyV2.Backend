using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class removingLinksToTheReturns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturn_Biller_BillerId",
                table: "SalesReturn");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturn_Customer_CustomerId",
                table: "SalesReturn");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturn_Sale_SaleId",
                table: "SalesReturn");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturn_Warehouse_WarehouseId",
                table: "SalesReturn");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturn_BillerId",
                table: "SalesReturn");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturn_CustomerId",
                table: "SalesReturn");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturn_SaleId",
                table: "SalesReturn");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturn_WarehouseId",
                table: "SalesReturn");

            migrationBuilder.DropColumn(
                name: "BillerId",
                table: "SalesReturn");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "SalesReturn");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "SalesReturn");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "SalesReturn");

            migrationBuilder.AddColumn<string>(
                name: "BillerName",
                table: "SalesReturn",
                type: "VARCHAR",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "SalesReturn",
                type: "VARCHAR",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WarehouseName",
                table: "SalesReturn",
                type: "VARCHAR",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillerName",
                table: "SalesReturn");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "SalesReturn");

            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "SalesReturn");

            migrationBuilder.AddColumn<long>(
                name: "BillerId",
                table: "SalesReturn",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "SalesReturn",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SaleId",
                table: "SalesReturn",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WarehouseId",
                table: "SalesReturn",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturn_BillerId",
                table: "SalesReturn",
                column: "BillerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturn_CustomerId",
                table: "SalesReturn",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturn_SaleId",
                table: "SalesReturn",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturn_WarehouseId",
                table: "SalesReturn",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturn_Biller_BillerId",
                table: "SalesReturn",
                column: "BillerId",
                principalTable: "Biller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturn_Customer_CustomerId",
                table: "SalesReturn",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturn_Sale_SaleId",
                table: "SalesReturn",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturn_Warehouse_WarehouseId",
                table: "SalesReturn",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
