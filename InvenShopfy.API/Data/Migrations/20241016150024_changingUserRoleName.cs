using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class changingUserRoleName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityRole_IdentityUser_CustomUserRequestId",
                table: "IdentityRole");

            migrationBuilder.DropIndex(
                name: "IX_IdentityRole_CustomUserRequestId",
                table: "IdentityRole");

            migrationBuilder.DropColumn(
                name: "CustomUserRequestId",
                table: "IdentityRole");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "IdentityUser",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "IdentityUser");

            migrationBuilder.AddColumn<long>(
                name: "CustomUserRequestId",
                table: "IdentityRole",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRole_CustomUserRequestId",
                table: "IdentityRole",
                column: "CustomUserRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRole_IdentityUser_CustomUserRequestId",
                table: "IdentityRole",
                column: "CustomUserRequestId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }
    }
}
