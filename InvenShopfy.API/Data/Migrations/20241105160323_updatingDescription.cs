using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class updatingDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PurchaseNote",
                table: "Expense",
                newName: "ExpenseNote");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Expense",
                newName: "ExpenseCost");

            migrationBuilder.AlterColumn<string>(
                name: "ExpenseType",
                table: "Expense",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");

            migrationBuilder.AddColumn<string>(
                name: "ExpenseDescription",
                table: "Expense",
                type: "VARCHAR(50)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseDescription",
                table: "Expense");

            migrationBuilder.RenameColumn(
                name: "ExpenseNote",
                table: "Expense",
                newName: "PurchaseNote");

            migrationBuilder.RenameColumn(
                name: "ExpenseCost",
                table: "Expense",
                newName: "Amount");

            migrationBuilder.AlterColumn<string>(
                name: "ExpenseType",
                table: "Expense",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");
        }
    }
}
