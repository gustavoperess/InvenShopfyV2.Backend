using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingSaleItemsSeven : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StafNote",
                table: "Sale",
                newName: "StaffNote");

            migrationBuilder.AlterColumn<long>(
                name: "TotalQuantitySoldPerProduct",
                table: "SaleProduct",
                type: "BIGINT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StaffNote",
                table: "Sale",
                newName: "StafNote");

            migrationBuilder.AlterColumn<int>(
                name: "TotalQuantitySoldPerProduct",
                table: "SaleProduct",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "BIGINT");
        }
    }
}
