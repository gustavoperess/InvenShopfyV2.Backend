using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class changingRequirmentsForRewardsPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RewardPoint",
                table: "Customer",
                type: "BIGINT",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "BIGINT",
                oldMaxLength: 30);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RewardPoint",
                table: "Customer",
                type: "BIGINT",
                maxLength: 30,
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "BIGINT",
                oldMaxLength: 30,
                oldNullable: true);
        }
    }
}
