using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingRemarkStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RemarkStatus",
                table: "SalesReturn",
                type: "VARCHAR",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "SalesReturn",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemarkStatus",
                table: "SalesReturn");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "SalesReturn");
        }
    }
}
