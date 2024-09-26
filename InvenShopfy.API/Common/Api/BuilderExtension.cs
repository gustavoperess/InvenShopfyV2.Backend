using InvenShopfy.API.Data;
using InvenShopfy.API.Handlers.Expenses;
using InvenShopfy.API.Handlers.People;
using InvenShopfy.API.Handlers.Products;
using InvenShopfy.API.Handlers.Tradings.Purchase;
using InvenShopfy.API.Handlers.Tradings.Sales;
using InvenShopfy.API.Handlers.Warehouses;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Handlers.Warehouse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace InvenShopfy.API.Common.Api;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
        
    }

    public static void CloudinaryConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
        var cloudinarySettings = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
        if (cloudinarySettings != null)
        {
            Configuration.CloudinarySettings = cloudinarySettings;
        } 
        else
        {
            throw new Exception("Cloudinary settings are not configured properly in appsettings.json.");
        }
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
        builder.Services.AddTransient<IWarehouseHandler, WarehouseHandler>();
        builder.Services.AddTransient<IPurchaseHandler, PurchaseHandler>();
        builder.Services.AddTransient<ISalesHandler, SaleHandler>();
      
    }
}