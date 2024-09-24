using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingItemsToSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Product_ProductId",
                table: "Sale");

            migrationBuilder.DropIndex(
                name: "IX_Sale_ProductId",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Sale");

            migrationBuilder.AlterColumn<double>(
                name: "TotalAmount",
                table: "Sale",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "MONEY");

            migrationBuilder.AlterColumn<double>(
                name: "ShippingCost",
                table: "Sale",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "MONEY");

            migrationBuilder.AlterColumn<short>(
                name: "RandomNumber",
                table: "Sale",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddColumn<int>(
                name: "TotalQuantitySold",
                table: "Sale",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SaleProduct",
                columns: table => new
                {
                    SaleId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    SingleQuantitySold = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPrice = table.Column<double>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleProduct", x => new { x.SaleId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_SaleProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleProduct_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_ProductId",
                table: "SaleProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleProduct");

            migrationBuilder.DropColumn(
                name: "TotalQuantitySold",
                table: "Sale");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Sale",
                type: "MONEY",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ShippingCost",
                table: "Sale",
                type: "MONEY",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "RandomNumber",
                table: "Sale",
                type: "INT",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "SMALLINT");

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "Sale",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Sale_ProductId",
                table: "Sale",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Product_ProductId",
                table: "Sale",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
