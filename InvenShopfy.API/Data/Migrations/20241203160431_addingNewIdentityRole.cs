using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingNewIdentityRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUser_IdentityRole_CustomIdentityRoleId",
                table: "IdentityUser");

            migrationBuilder.DropIndex(
                name: "IX_IdentityUser_CustomIdentityRoleId",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "CustomIdentityRoleId",
                table: "IdentityUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomIdentityRoleId",
                table: "IdentityUser",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUser_CustomIdentityRoleId",
                table: "IdentityUser",
                column: "CustomIdentityRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUser_IdentityRole_CustomIdentityRoleId",
                table: "IdentityUser",
                column: "CustomIdentityRoleId",
                principalTable: "IdentityRole",
                principalColumn: "Id");
        }
    }
}
