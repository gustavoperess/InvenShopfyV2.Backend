using InvenShopfy.API.Data;
using InvenShopfy.API.Handlers;
using InvenShopfy.API.Handlers.Expenses;
using InvenShopfy.API.Handlers.People;
using InvenShopfy.API.Handlers.Products;
using InvenShopfy.API.Handlers.UserManagement;
using InvenShopfy.API.Handlers.Warehouses;
// using InvenShopfy.API.Handlers;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Handlers.Warehouse;
using InvenShopfy.Core.Models.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace InvenShopfy.API.Common.Api;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        // Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        // Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }
    
    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });
    }
    
    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        
        builder.Services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();

        builder.Services.AddAuthorization();

    }
    
    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(
            x => x.UseNpgsql(Configuration.ConnectionString));
    
        builder.Services.AddIdentityCore<User>().AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();
    
    }
    
    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options => options.AddPolicy(Configuration.CorsPolicyName,
            policy =>
                policy.WithOrigins([
                    Configuration.BackendUrl,
                    Configuration.FrontendUrl
                ]).AllowAnyHeader().AllowAnyMethod().AllowCredentials()
        ));
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IProductHandler, ProductHandler>();
        builder.Services.AddTransient<IBrandHandler, BrandHandler>();
        builder.Services.AddTransient<IUnitHandler, UnitHandler>();
        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        builder.Services.AddTransient<IExpenseCategoryHandler, ExpenseCategoryHandler>();
        builder.Services.AddTransient<IExpenseHandler, ExpenseHandler>();
        builder.Services.AddTransient<IBillerHandler, BillerHandler>();
        builder.Services.AddTransient<ICustomerHandler, CustomerHandler>();
        builder.Services.AddTransient<ISupplierHandler, SupplierHandler>();
        builder.Services.AddTransient<IUserManagementRoleHandler, RolerHandler>();
        builder.Services.AddTransient<IUserManagementUserHandler, UserHandler>();
        builder.Services.AddTransient<IWarehouseHandler, WarehouseHandler>();
      
    }
}