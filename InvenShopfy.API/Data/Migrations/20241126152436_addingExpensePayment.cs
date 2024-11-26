using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingExpensePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpensePayment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExpenseId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpensePaymentDescription = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    PaymentType = table.Column<string>(type: "text", nullable: false),
                    CardNumber = table.Column<string>(type: "text", nullable: false),
                    ExpenseCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ExpenseNote = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ExpenseStatus = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensePayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpensePayment_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpensePayment_ExpenseId",
                table: "ExpensePayment",
                column: "ExpenseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpensePayment");
        }
    }
}
