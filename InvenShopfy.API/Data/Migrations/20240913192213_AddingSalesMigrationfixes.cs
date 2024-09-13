using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class AddingSalesMigrationfixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RandomNumber",
                table: "Sale",
                type: "INT",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "SMALLINT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "RandomNumber",
                table: "Sale",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");
        }
    }
}
