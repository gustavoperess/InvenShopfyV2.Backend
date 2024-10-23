using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class testingRoleName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "IdentityUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "IdentityUser",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
