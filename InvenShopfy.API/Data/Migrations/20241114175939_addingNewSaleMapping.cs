using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingNewSaleMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Biller_BillerId",
                table: "Sale");

            migrationBuilder.DropIndex(
                name: "IX_Sale_BillerId",
                table: "Sale");

            migrationBuilder.AlterColumn<int>(
                name: "BillerId",
                table: "Sale",
                type: "INT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BillerId",
                table: "Sale",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_BillerId",
                table: "Sale",
                column: "BillerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Biller_BillerId",
                table: "Sale",
                column: "BillerId",
                principalTable: "Biller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
