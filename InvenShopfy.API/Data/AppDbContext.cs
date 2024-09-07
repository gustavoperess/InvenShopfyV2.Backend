using System.Reflection;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Models.UserManagement;
using InvenShopfy.Core.Models.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Unit> Unit { get; set; } = null!;
        
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; } = null!;
        public DbSet<Expense> Expenses { get; set; } = null!;
        
        public DbSet<Biller> Billers { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        
        public DbSet<Warehouse> Warehouses { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder); // Call this if you're using IdentityDbContext
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}