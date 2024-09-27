using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class fixingAllEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "User",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "SMALLINT");

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseStatus",
                table: "Purchase",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "SMALLINT");

            migrationBuilder.AlterColumn<string>(
                name: "ExpenseType",
                table: "Expense",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "SMALLINT");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerGroup",
                table: "Customer",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "SMALLINT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Gender",
                table: "User",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");

            migrationBuilder.AlterColumn<short>(
                name: "PurchaseStatus",
                table: "Purchase",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");

            migrationBuilder.AlterColumn<short>(
                name: "ExpenseType",
                table: "Expense",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");

            migrationBuilder.AlterColumn<short>(
                name: "CustomerGroup",
                table: "Customer",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");
        }
    }
}
