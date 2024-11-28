using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class changingStructureOfRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserRole",
                table: "IdentityUserRole");

            migrationBuilder.AddColumn<long>(
                name: "CustomIdentityRoleId",
                table: "IdentityUser",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RoleId",
                table: "IdentityUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserRole",
                table: "IdentityUserRole",
                column: "UserId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUser_IdentityRole_CustomIdentityRoleId",
                table: "IdentityUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserRole",
                table: "IdentityUserRole");

            migrationBuilder.DropIndex(
                name: "IX_IdentityUser_CustomIdentityRoleId",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "CustomIdentityRoleId",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "IdentityUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserRole",
                table: "IdentityUserRole",
                columns: new[] { "UserId", "RoleId" });
        }
    }
}
