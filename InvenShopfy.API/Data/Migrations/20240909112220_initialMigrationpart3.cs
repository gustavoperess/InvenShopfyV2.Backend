using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class initialMigrationpart3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    BrandImage = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    SubCategory = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    MainCategory = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    City = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Country = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Address = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    ZipCode = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    RewardPoint = table.Column<long>(type: "BIGINT", maxLength: 30, nullable: false),
                    CustomerGroup = table.Column<short>(type: "SMALLINT", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "VARCHAR", maxLength: 180, nullable: false),
                    SubCategory = table.Column<string>(type: "VARCHAR", maxLength: 180, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    SupplierCode = table.Column<long>(type: "BIGINT", maxLength: 30, nullable: false),
                    Country = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    City = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Address = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    ZipCode = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    Company = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    ShortName = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WarehouseName = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    Address = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    ZipCode = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
                    DateOfJoin = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Gender = table.Column<short>(type: "SMALLINT", nullable: false),
                    Username = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    ProfileImage = table.Column<string>(type: "VARCHAR", nullable: true),
                    Password = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Price = table.Column<decimal>(type: "MONEY", nullable: false),
                    ProductCode = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Quantity = table.Column<long>(type: "BIGINT", nullable: false),
                    CreateAt = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    UnitId = table.Column<long>(type: "bigint", nullable: false),
                    BrandId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ProductImage = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Biller",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
                    DateOfJoin = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Identification = table.Column<string>(type: "VARCHAR", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    Country = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    ZipCode = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    BillerCode = table.Column<long>(type: "BIGINT", maxLength: 160, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biller_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    ExpenseType = table.Column<short>(type: "SMALLINT", nullable: false),
                    ExpenseCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    VoucherNumber = table.Column<long>(type: "BIGINT", nullable: false),
                    Amount = table.Column<double>(type: "numeric(18,2)", nullable: false),
                    PurchaseNote = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expense_ExpenseCategory_ExpenseCategoryId",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "ExpenseCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expense_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Biller_WarehouseId",
                table: "Biller",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ExpenseCategoryId",
                table: "Expense",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_WarehouseId",
                table: "Expense",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitId",
                table: "Product",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biller");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ExpenseCategory");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
