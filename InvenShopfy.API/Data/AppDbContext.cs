using System.Reflection;
using InvenShopfy.API.Models;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Models.Messages;
using InvenShopfy.Core.Models.Notifications;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Models.Tradings.Purchase;
using InvenShopfy.Core.Models.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Models.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Models.Tradings.Sales;
using InvenShopfy.Core.Models.Transfer;
using InvenShopfy.Core.Models.Warehouse;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Data;

    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : IdentityDbContext<CustomUserRequest,CustomIdentityRole, long,
            IdentityUserClaim<long>,
            IdentityUserRole<long>,
            IdentityUserLogin<long>,
            IdentityRoleClaim<long>,
            IdentityUserToken<long>>(options)
    {
       
        public DbSet<SaleReturn> SaleReturns { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Unit> Unit { get; set; } = null!;
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; } = null!;
        public DbSet<Expense> Expenses { get; set; } = null!;
        public DbSet<Biller> Billers { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public DbSet<AddPurchase> Purchases { get; set; } = null!;
        public DbSet<Warehouse> Warehouses { get; set; } = null!;
        
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<WarehouseProduct> WarehousesProducts { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;
        public DbSet<SaleProduct> SaleProducts { get; set; } = null!;
        public DbSet<Transfer> Transfers { get; set; } = null!;
        public DbSet<PurchaseReturn> PurchaseReturns { get; set; } = null!;
        public DbSet<PurchaseProduct> PurchaseProducts { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
