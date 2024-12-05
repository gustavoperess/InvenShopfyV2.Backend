using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class changingUseRoleMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserRole",
                table: "IdentityUserRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserRole",
                table: "IdentityUserRole",
                columns: new[] { "UserId", "RoleId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserRole",
                table: "IdentityUserRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserRole",
                table: "IdentityUserRole",
                column: "UserId");
        }
    }
}
