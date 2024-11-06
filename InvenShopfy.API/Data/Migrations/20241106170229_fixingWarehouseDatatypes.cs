using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class fixingWarehouseDatatypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Purchase_PurchaseId",
                table: "Transfer");

            migrationBuilder.DropIndex(
                name: "IX_Transfer_PurchaseId",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "FromWarehouse",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "ToWarehouse",
                table: "Transfer");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Transfer",
                type: "INT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL");

            migrationBuilder.AddColumn<long>(
                name: "FromWarehouseId",
                table: "Transfer",
                type: "BIGINT",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ToWarehouseId",
                table: "Transfer",
                type: "BIGINT",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromWarehouseId",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "ToWarehouseId",
                table: "Transfer");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Transfer",
                type: "DECIMAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddColumn<string>(
                name: "FromWarehouse",
                table: "Transfer",
                type: "VARCHAR",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "PurchaseId",
                table: "Transfer",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ToWarehouse",
                table: "Transfer",
                type: "VARCHAR",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_PurchaseId",
                table: "Transfer",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_Purchase_PurchaseId",
                table: "Transfer",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
