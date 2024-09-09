using System.Reflection;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Models.UserManagement;
using InvenShopfy.Core.Models.Warehouse;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User = InvenShopfy.API.Models.User;

namespace InvenShopfy.API.Data;

    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : IdentityDbContext<User, 
            IdentityRole<long>,
            long,
            IdentityUserClaim<long>,
            IdentityUserRole<long>,
            IdentityUserLogin<long>,
            IdentityRoleClaim<long>,
            IdentityUserToken<long>>(options)
    {
       
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Unit> Unit { get; set; } = null!;
        
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; } = null!;
        public DbSet<Expense> Expenses { get; set; } = null!;
        
        public DbSet<Biller> Billers { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        
        // public DbSet<Role> Roles { get; set; } = null!;
        // public DbSet<User> Users { get; set; } = null!;
        
        public DbSet<Warehouse> Warehouses { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
