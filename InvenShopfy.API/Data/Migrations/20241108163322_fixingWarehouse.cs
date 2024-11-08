using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class fixingWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WarehouseProduct");

            migrationBuilder.AlterColumn<long>(
                name: "ToWarehouseId",
                table: "Transfer",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "BIGINT");

            migrationBuilder.AlterColumn<long>(
                name: "FromWarehouseId",
                table: "Transfer",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "BIGINT");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_FromWarehouseId",
                table: "Transfer",
                column: "FromWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_ToWarehouseId",
                table: "Transfer",
                column: "ToWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_Warehouse_FromWarehouseId",
                table: "Transfer",
                column: "FromWarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_Warehouse_ToWarehouseId",
                table: "Transfer",
                column: "ToWarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Warehouse_FromWarehouseId",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Warehouse_ToWarehouseId",
                table: "Transfer");

            migrationBuilder.DropIndex(
                name: "IX_Transfer_FromWarehouseId",
                table: "Transfer");

            migrationBuilder.DropIndex(
                name: "IX_Transfer_ToWarehouseId",
                table: "Transfer");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WarehouseProduct",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "ToWarehouseId",
                table: "Transfer",
                type: "BIGINT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "FromWarehouseId",
                table: "Transfer",
                type: "BIGINT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
