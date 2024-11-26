using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class updatingMigrationsForExpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseCategoryId",
                table: "ExpensePayment");

            migrationBuilder.AlterColumn<string>(
                name: "VoucherNumber",
                table: "Expense",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "BIGINT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExpenseCategoryId",
                table: "ExpensePayment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "VoucherNumber",
                table: "Expense",
                type: "BIGINT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");
        }
    }
}
