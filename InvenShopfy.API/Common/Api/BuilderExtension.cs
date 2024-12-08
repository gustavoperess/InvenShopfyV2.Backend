using InvenShopfy.API.Common.CloudinaryServiceNamespace;
using InvenShopfy.API.Data;
using Serilog;
using InvenShopfy.API.Handlers.Expenses;
using InvenShopfy.API.Handlers.Messages;
using InvenShopfy.API.Handlers.Notifications;
using InvenShopfy.API.Handlers.People;
using InvenShopfy.API.Handlers.Products;
using InvenShopfy.API.Handlers.Reports;
using InvenShopfy.API.Handlers.Tradings.Purchase;
using InvenShopfy.API.Handlers.Tradings.Returns;
using InvenShopfy.API.Handlers.Tradings.Sales;
using InvenShopfy.API.Handlers.Transfers;
using InvenShopfy.API.Handlers.Warehouses;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Handlers.Messages;
using InvenShopfy.Core.Handlers.Notifications;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.Reports;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Handlers.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Handlers.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Handlers.Transfer;
using InvenShopfy.Core.Handlers.Warehouse;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;


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
            throw new Exception("Cloudinary settings are not properly configured. " +
                                "Please check appsettings.json and ensure that 'CloudinarySettings' " +
                                "section is present and filled correctly.");
        }
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });
    }

    // public static void AddSecurity(this WebApplicationBuilder builder)
    // {
    //     builder.Services
    //         .AddAuthentication(IdentityConstants.ApplicationScheme)
    //         .AddIdentityCookies();
    //
    //     builder.Services.AddAuthorization();
    //     
    // }
    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddCookie(options =>
            {
           
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Expiration = TimeSpan.FromDays(1);  
            });

        builder.Services.AddAuthorization();
    }


    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(
            x => x.UseNpgsql(Configuration.ConnectionString));

        builder.Services.AddIdentityCore<CustomUserRequest>().AddRoles<CustomIdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options => options.AddPolicy(Configuration.CorsPolicyName,
            policy =>
                policy.WithOrigins(
                        Configuration.BackendUrl,  
                        Configuration.FrontendUrl   
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()  // Allow cookies to be sent with requests
        ));
    }
    
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        // Define a shared filter for logs with StatusCode >= 400
        Func<LogEvent, bool> isErrorOrStatusCode = logEvent =>
            logEvent.Properties.ContainsKey("StatusCode") &&
            logEvent.Properties["StatusCode"] is ScalarValue scalarValue &&
            scalarValue.Value is int statusCode &&
            statusCode >= 400;

        // Configure the main logger
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            // File sink for all logs
            .WriteTo.File(
                "logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} StatusCode: {StatusCode} {NewLine}{Exception}"
            )
            // Console sink for filtered logs (errors only)
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} StatusCode: {StatusCode}] {Message}{NewLine}{Exception}",
                restrictedToMinimumLevel: LogEventLevel.Information
            )
            .Filter.ByIncludingOnly(isErrorOrStatusCode) // Apply the filter to the console sink
            .CreateLogger();

        // Add Serilog to the builder
        builder.Host.UseSerilog();
    }


    public static void AddServices(this WebApplicationBuilder builder)
    {
        // builder.Services.AddTransient<IProductHandler, ProductHandler>();
        // builder.Services.AddTransient<IBrandHandler, BrandHandler>();
        // builder.Services.AddTransient<IUnitHandler, UnitHandler>();
        // builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        // builder.Services.AddTransient<IExpenseCategoryHandler, ExpenseCategoryHandler>();
        // builder.Services.AddTransient<IExpenseHandler, ExpenseHandler>();
        // builder.Services.AddTransient<IBillerHandler, BillerHandler>();
        // builder.Services.AddTransient<ICustomerHandler, CustomerHandler>();
        // builder.Services.AddTransient<ISupplierHandler, SupplierHandler>();
        // builder.Services.AddTransient<IWarehouseHandler, WarehouseHandler>();
        // builder.Services.AddTransient<IPurchaseHandler, PurchaseHandler>();
        // builder.Services.AddTransient<ISalesHandler, SaleHandler>();
        // builder.Services.AddTransient<ISalesReturnHandler, SalesReturnHandlers>(); 
        // builder.Services.AddTransient<IPurchaseReturnHandler, PurchaseReturnHandlers>(); 
        // builder.Services.AddTransient<ITransferHandler, TransferHandler>();
        // builder.Services.AddTransient<INotificationHandler, NotificationHandlers>();
        // builder.Services.AddTransient<IMessageHandler, MessageHandler>(); // new
        // builder.Services.AddTransient<IReportHandler, ReportHandler>();
        // TESTING WITH SCOPED 

        builder.Services.AddScoped<IProductHandler, ProductHandler>();
        builder.Services.AddScoped<IBrandHandler, BrandHandler>();
        builder.Services.AddScoped<IUnitHandler, UnitHandler>();
        builder.Services.AddScoped<ICategoryHandler, CategoryHandler>();
        builder.Services.AddScoped<IExpenseCategoryHandler, ExpenseCategoryHandler>();
        builder.Services.AddScoped<IExpenseHandler, ExpenseHandler>();
        builder.Services.AddScoped<IBillerHandler, BillerHandler>();
        builder.Services.AddScoped<ICustomerHandler, CustomerHandler>();
        builder.Services.AddScoped<ISupplierHandler, SupplierHandler>();
        builder.Services.AddScoped<IWarehouseHandler, WarehouseHandler>();
        builder.Services.AddScoped<IPurchaseHandler, PurchaseHandler>();
        builder.Services.AddScoped<ISalesHandler, SaleHandler>();
        builder.Services.AddScoped<ISalesReturnHandler, SalesReturnHandlers>();
        builder.Services.AddScoped<IPurchaseReturnHandler, PurchaseReturnHandlers>();
        builder.Services.AddScoped<ITransferHandler, TransferHandler>();
        builder.Services.AddScoped<INotificationHandler, NotificationHandlers>();
        builder.Services.AddScoped<IExpensePaymentHandler, ExpensePaymentHandler>();
        builder.Services.AddScoped<ISalesPaymentHandler, SalesPaymentHandler>();
        builder.Services.AddScoped<IMessageHandler, MessageHandler>();
        builder.Services.AddScoped<IReportHandler, ReportHandler>();


        builder.Services.AddTransient<CloudinaryService>();
    }
}