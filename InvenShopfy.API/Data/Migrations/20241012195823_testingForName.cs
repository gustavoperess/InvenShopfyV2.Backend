using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class testingForName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityRole_IdentityUser_UserId",
                table: "IdentityRole");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "IdentityRole",
                newName: "CustomUserRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityRole_UserId",
                table: "IdentityRole",
                newName: "IX_IdentityRole_CustomUserRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRole_IdentityUser_CustomUserRequestId",
                table: "IdentityRole",
                column: "CustomUserRequestId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityRole_IdentityUser_CustomUserRequestId",
                table: "IdentityRole");

            migrationBuilder.RenameColumn(
                name: "CustomUserRequestId",
                table: "IdentityRole",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityRole_CustomUserRequestId",
                table: "IdentityRole",
                newName: "IX_IdentityRole_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRole_IdentityUser_UserId",
                table: "IdentityRole",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }
    }
}
