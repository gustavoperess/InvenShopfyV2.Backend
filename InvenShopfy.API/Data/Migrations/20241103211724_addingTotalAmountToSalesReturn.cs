using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingTotalAmountToSalesReturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "SalesReturn",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "SalesReturn",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");
        }
    }
}
