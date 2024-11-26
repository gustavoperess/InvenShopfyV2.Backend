using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class fixingDataForExpensePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpensePaymentDescription",
                table: "ExpensePayment");

            migrationBuilder.DropColumn(
                name: "ExpenseStatus",
                table: "ExpensePayment");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "ExpensePayment",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "ExpensePayment",
                type: "VARCHAR",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "ExpensePayment",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "ExpensePayment",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "ExpensePaymentDescription",
                table: "ExpensePayment",
                type: "VARCHAR(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpenseStatus",
                table: "ExpensePayment",
                type: "VARCHAR(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
