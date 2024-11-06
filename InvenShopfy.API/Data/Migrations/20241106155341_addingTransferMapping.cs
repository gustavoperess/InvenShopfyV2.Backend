using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingTransferMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityOfItems",
                table: "Warehouse",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Transfer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransferDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ProductName = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    ReferenceNumber = table.Column<string>(type: "VARCHAR", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    PurchaseId = table.Column<long>(type: "bigint", nullable: false),
                    AuthorizedBy = table.Column<string>(type: "VARCHAR", nullable: false),
                    Reason = table.Column<string>(type: "VARCHAR", maxLength: 180, nullable: false),
                    FromWarehouse = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    ToWarehouse = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    TransferStatus = table.Column<string>(type: "VARCHAR", nullable: false),
                    TransferNote = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfer_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_PurchaseId",
                table: "Transfer",
                column: "PurchaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transfer");

            migrationBuilder.DropColumn(
                name: "QuantityOfItems",
                table: "Warehouse");
        }
    }
}
