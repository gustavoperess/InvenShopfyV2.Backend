using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingNewWarehouseProductTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseProduct",
                table: "WarehouseProduct");

            migrationBuilder.AddColumn<long>(
                name: "WarehouseId",
                table: "WarehouseProduct",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseProduct",
                table: "WarehouseProduct",
                columns: new[] { "ProductId", "WarehouseId" });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProduct_WarehouseId",
                table: "WarehouseProduct",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseProduct_Warehouse_WarehouseId",
                table: "WarehouseProduct",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseProduct_Warehouse_WarehouseId",
                table: "WarehouseProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseProduct",
                table: "WarehouseProduct");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseProduct_WarehouseId",
                table: "WarehouseProduct");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "WarehouseProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseProduct",
                table: "WarehouseProduct",
                column: "ProductId");
        }
    }
}
