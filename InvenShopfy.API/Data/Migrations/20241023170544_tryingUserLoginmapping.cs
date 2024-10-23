using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class tryingUserLoginmapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserLogin",
                table: "IdentityUserLogin");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderDisplayName",
                table: "IdentityUserLogin",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "IdentityUserLogin",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "IdentityUserLogin",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "IdentityUserLogin",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "IdentityUserLogin",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "IdentityUserLogin",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserLogin",
                table: "IdentityUserLogin",
                columns: new[] { "UserName", "Email", "Password" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserLogin",
                table: "IdentityUserLogin");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "IdentityUserLogin");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "IdentityUserLogin");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "IdentityUserLogin");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "IdentityUserLogin",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderDisplayName",
                table: "IdentityUserLogin",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "IdentityUserLogin",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserLogin",
                table: "IdentityUserLogin",
                columns: new[] { "LoginProvider", "ProviderKey" });
        }
    }
}
