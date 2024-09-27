using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSaleStatusColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SaleStatus",
                table: "Sale",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "SMALLINT");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "Sale",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "SMALLINT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "SaleStatus",
                table: "Sale",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");

            migrationBuilder.AlterColumn<short>(
                name: "PaymentStatus",
                table: "Sale",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");
        }
    }
}
