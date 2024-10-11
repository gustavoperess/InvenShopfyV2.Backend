using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class updatingWarehouseDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Warehouse",
                newName: "WarehouseZipCode");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Warehouse",
                newName: "WarehousePhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Warehouse",
                newName: "WarehouseEmail");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Warehouse",
                newName: "WarehouseCity");

            migrationBuilder.AddColumn<string>(
                name: "WarehouseCountry",
                table: "Warehouse",
                type: "VARCHAR",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WarehouseOpeningNotes",
                table: "Warehouse",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarehouseCountry",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "WarehouseOpeningNotes",
                table: "Warehouse");

            migrationBuilder.RenameColumn(
                name: "WarehouseZipCode",
                table: "Warehouse",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "WarehousePhoneNumber",
                table: "Warehouse",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "WarehouseEmail",
                table: "Warehouse",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "WarehouseCity",
                table: "Warehouse",
                newName: "Address");
        }
    }
}
