using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingTaxForProductsTree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfitLine",
                table: "Sale");

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxAmount",
                table: "Sale",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddColumn<decimal>(
                name: "ProfitAmount",
                table: "Sale",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfitAmount",
                table: "Sale");

            migrationBuilder.AlterColumn<int>(
                name: "TaxAmount",
                table: "Sale",
                type: "INT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "ProfitLine",
                table: "Sale",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }
    }
}
