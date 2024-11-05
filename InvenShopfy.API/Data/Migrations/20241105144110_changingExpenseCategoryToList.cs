using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    public partial class changingExpenseCategoryToList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Use raw SQL to alter the column to type `text[]`, with explicit casting
            migrationBuilder.Sql(
                "ALTER TABLE \"ExpenseCategory\" ALTER COLUMN \"SubCategory\" TYPE text[] USING \"SubCategory\"::text[];"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Use raw SQL to revert the column back to VARCHAR
            migrationBuilder.Sql(
                "ALTER TABLE \"ExpenseCategory\" ALTER COLUMN \"SubCategory\" TYPE VARCHAR(180) USING \"SubCategory\"[1];"
            );
        }
    }
}