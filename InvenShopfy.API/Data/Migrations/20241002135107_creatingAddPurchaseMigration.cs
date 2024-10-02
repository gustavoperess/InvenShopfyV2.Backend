using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class creatingAddPurchaseMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Purchase");

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "Purchase",
                type: "TIMESTAMP",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                table: "Purchase",
                type: "VARCHAR",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalQuantityBought",
                table: "Purchase",
                type: "numeric(18,2)",
                maxLength: 80,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PurchaseProduct",
                columns: table => new
                {
                    PurchaseId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    AddPurchaseId = table.Column<long>(type: "bigint", nullable: false),
                    TotalQuantityBoughtPerProduct = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPricePaidPerProduct = table.Column<double>(type: "numeric(18,2)", nullable: false),
                    PurchaseReferenceNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseProduct", x => new { x.PurchaseId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_PurchaseProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseProduct_Purchase_AddPurchaseId",
                        column: x => x.AddPurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProduct_AddPurchaseId",
                table: "PurchaseProduct",
                column: "AddPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProduct_ProductId",
                table: "PurchaseProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseProduct");

            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "TotalQuantityBought",
                table: "Purchase");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Purchase",
                type: "TIMESTAMPTZ",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
