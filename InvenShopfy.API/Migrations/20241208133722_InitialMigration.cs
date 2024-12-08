using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                    BrandName = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    BrandImage = table.Column<string>(type: "TEXT", nullable: true)
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
                    MainCategory = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    SubCategory = table.Column<List<string>>(type: "text[]", nullable: false)
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
                    CustomerName = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    City = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Country = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Address = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    ZipCode = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    RewardPoint = table.Column<long>(type: "BIGINT", maxLength: 30, nullable: true),
                    CustomerGroup = table.Column<string>(type: "VARCHAR(50)", nullable: false),
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
                    MainCategory = table.Column<string>(type: "VARCHAR", maxLength: 180, nullable: false),
                    SubCategory = table.Column<List<string>>(type: "text[]", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ClaimValue = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateOfJoin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProfilePicture = table.Column<string>(type: "character varying(70000)", maxLength: 70000, nullable: true),
                    Gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    LastActivityTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    Email = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessageTitle = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: true),
                    Subject = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: true),
                    ToUserId = table.Column<long>(type: "BIGINT", nullable: false),
                    MessageBody = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Time = table.Column<DateOnly>(type: "date", nullable: false),
                    IsImportant = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    IsDeleted = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    IsSent = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    IsReceived = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Urgency = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    NotificationTitle = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
                    From = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
                    CreateAt = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    Href = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReferenceNumber = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    SupplierName = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    WarehouseName = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ReturnNote = table.Column<string>(type: "TEXT", nullable: false),
                    RemarkStatus = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReturn", x => x.Id);
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
                name: "SalesReturn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReferenceNumber = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    BillerName = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    CustomerName = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    WarehouseName = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    ReturnTotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ReturnNote = table.Column<string>(type: "TEXT", nullable: false),
                    RemarkStatus = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReturn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplierName = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
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
                    UnitName = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    UnitShortName = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false)
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
                    WarehousePhoneNumber = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    WarehouseEmail = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    WarehouseCity = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    WarehouseCountry = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    WarehouseZipCode = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    WarehouseOpeningNotes = table.Column<string>(type: "TEXT", nullable: true),
                    QuantityOfItems = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    EntityType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsAllowed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ClaimValue = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityClaim_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserRole",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityUserRole_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserToken",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Name = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_IdentityUserToken_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
                    DateOfJoin = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Gender = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Username = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    ProfileImage = table.Column<string>(type: "TEXT", nullable: true),
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
                    ProductName = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    ProductPrice = table.Column<decimal>(type: "MONEY", nullable: false),
                    ProductCode = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    StockQuantity = table.Column<long>(type: "BIGINT", nullable: false),
                    CreateAt = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    UnitId = table.Column<long>(type: "bigint", nullable: false),
                    BrandId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ProductImage = table.Column<string>(type: "TEXT", nullable: true),
                    TaxPercentage = table.Column<int>(type: "INT", nullable: false),
                    MarginRange = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    SubCategory = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    Featured = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    Expired = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    Sale = table.Column<bool>(type: "BOOLEAN", nullable: false)
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
                    DateOfJoin = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Identification = table.Column<string>(type: "VARCHAR", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    Country = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    ZipCode = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    BillerCode = table.Column<long>(type: "BIGINT", maxLength: 160, nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
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
                    ExpenseDescription = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    ExpenseType = table.Column<string>(type: "VARCHAR", nullable: false),
                    ExpenseCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ExpenseCost = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ExpenseNote = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    VoucherNumber = table.Column<string>(type: "VARCHAR", nullable: false),
                    ExpenseStatus = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PurchaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: false),
                    PurchaseStatus = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "numeric(18,2)", maxLength: 80, nullable: false),
                    PurchaseNote = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    TotalTax = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "VARCHAR", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    TotalAmountBought = table.Column<decimal>(type: "numeric(18,2)", maxLength: 80, nullable: false),
                    TotalNumberOfProductsBought = table.Column<long>(type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchase_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchase_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SaleDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    BillerId = table.Column<int>(type: "INT", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SaleStatus = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    SaleNote = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    StaffNote = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    TotalQuantitySold = table.Column<int>(type: "integer", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "VARCHAR", nullable: false),
                    Discount = table.Column<int>(type: "INT", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sale_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sale_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransferDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "VARCHAR", nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    AuthorizedBy = table.Column<string>(type: "VARCHAR", nullable: false),
                    Reason = table.Column<string>(type: "VARCHAR", maxLength: 180, nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    FromWarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    ToWarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    TransferStatus = table.Column<string>(type: "VARCHAR", nullable: false),
                    TransferNote = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfer_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfer_Warehouse_FromWarehouseId",
                        column: x => x.FromWarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfer_Warehouse_ToWarehouseId",
                        column: x => x.ToWarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseProduct",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseProduct", x => new { x.ProductId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_WarehouseProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseProduct_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpensePayment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExpenseId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    PaymentType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    CardNumber = table.Column<string>(type: "VARCHAR", maxLength: 30, nullable: false),
                    ExpenseNote = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensePayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpensePayment_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseProduct",
                columns: table => new
                {
                    AddPurchaseId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    TotalInTaxPaidPerProduct = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalQuantityBoughtPerProduct = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPricePaidPerProduct = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PurchaseReferenceNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseProduct", x => new { x.AddPurchaseId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_PurchaseProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseProduct_Purchase_AddPurchaseId",
                        column: x => x.AddPurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleProduct",
                columns: table => new
                {
                    SaleId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    TotalQuantitySoldPerProduct = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPricePerProduct = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleProduct", x => new { x.SaleId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_SaleProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleProduct_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesPayment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalesId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    PaymentType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    CardNumber = table.Column<string>(type: "VARCHAR", maxLength: 30, nullable: false),
                    SalesNote = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesPayment_Sale_SalesId",
                        column: x => x.SalesId,
                        principalTable: "Sale",
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
                name: "IX_ExpensePayment_ExpenseId",
                table: "ExpensePayment",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityClaim_UserId",
                table: "IdentityClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRole_NormalizedName",
                table: "IdentityRole",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUser_NormalizedEmail",
                table: "IdentityUser",
                column: "NormalizedEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUser_NormalizedUserName",
                table: "IdentityUser",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserLogin_UserId",
                table: "IdentityUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserRole_RoleId",
                table: "IdentityUserRole",
                column: "RoleId");

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
                name: "IX_Purchase_SupplierId",
                table: "Purchase",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_WarehouseId",
                table: "Purchase",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProduct_ProductId",
                table: "PurchaseProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_CustomerId",
                table: "Sale",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_WarehouseId",
                table: "Sale",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_ProductId",
                table: "SaleProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesPayment_SalesId",
                table: "SalesPayment",
                column: "SalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_FromWarehouseId",
                table: "Transfer",
                column: "FromWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_ProductId",
                table: "Transfer",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_ToWarehouseId",
                table: "Transfer",
                column: "ToWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProduct_WarehouseId",
                table: "WarehouseProduct",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biller");

            migrationBuilder.DropTable(
                name: "ExpensePayment");

            migrationBuilder.DropTable(
                name: "IdentityClaim");

            migrationBuilder.DropTable(
                name: "IdentityRoleClaim");

            migrationBuilder.DropTable(
                name: "IdentityUserLogin");

            migrationBuilder.DropTable(
                name: "IdentityUserRole");

            migrationBuilder.DropTable(
                name: "IdentityUserToken");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PurchaseProduct");

            migrationBuilder.DropTable(
                name: "PurchaseReturn");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SaleProduct");

            migrationBuilder.DropTable(
                name: "SalesPayment");

            migrationBuilder.DropTable(
                name: "SalesReturn");

            migrationBuilder.DropTable(
                name: "Transfer");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "WarehouseProduct");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ExpenseCategory");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Unit");
        }
    }
}
