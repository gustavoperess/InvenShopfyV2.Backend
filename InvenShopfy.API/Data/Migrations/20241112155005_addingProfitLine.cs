using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingProfitLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Sale");

            migrationBuilder.AddColumn<int>(
                name: "ProfitLine",
                table: "Sale",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TaxAmount",
                table: "Sale",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfitLine",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "Sale");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Sale",
                type: "VARCHAR(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
