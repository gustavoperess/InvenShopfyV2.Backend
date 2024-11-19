using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingMessageDatabaseTree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"Message\" ALTER COLUMN \"ToUserId\" TYPE BIGINT USING \"ToUserId\"::BIGINT");

            migrationBuilder.AlterColumn<long>(
                name: "ToUserId",
                table: "Message",
                type: "BIGINT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldMaxLength: 160);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"Message\" ALTER COLUMN \"ToUserId\" TYPE VARCHAR(160) USING \"ToUserId\"::VARCHAR");

            migrationBuilder.AlterColumn<string>(
                name: "ToUserId",
                table: "Message",
                type: "VARCHAR",
                maxLength: 160,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "BIGINT");
        }
    }
}