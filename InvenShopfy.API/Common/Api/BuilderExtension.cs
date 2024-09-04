using Microsoft.AspNetCore.Identity;
using InvenShopfy.Core;
using Microsoft.EntityFrameworkCore;


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
    
    // public static void AddDataContexts(this WebApplicationBuilder builder)
    // {
    //     builder.Services.AddDbContext<AppDbContext>(
    //         x => x.UseNpgsql(Configuration.ConnectionString));
    //
    //     builder.Services.AddIdentityCore<User>().AddRoles<IdentityRole<long>>()
    //         .AddEntityFrameworkStores<AppDbContext>()
    //         .AddApiEndpoints();
    //
    // }
    
    // public static void AddCrossOrigin(this WebApplicationBuilder builder)
    // {
    //     builder.Services.AddCors(options => options.AddPolicy(Configuration.CorsPolicyName,
    //         policy =>
    //             policy.WithOrigins([
    //                 Configuration.BackendUrl,
    //                 Configuration.FrontendUrl
    //             ]).AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    //     ));
    // }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        // builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        // builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
    }
}