using InvenShopfy.API.Common.Api;
using InvenShopfy.API.EndPoints.Products;

namespace InvenShopfy.API.EndPoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/").WithTags("Health Check").MapGet("/", () => new { message = "Ok" });

        endpoints.MapGroup("v2/products")
            .WithTags("Products")
            .MapEndpoint<CreateProductEndpoint>();
        
        endpoints.MapGroup("v2/brands")
            .WithTags("Brands");
            // .MapEndpoint<CreateProductEndPoint>();
        
        endpoints.MapGroup("v2/units")
            .WithTags("Products");
            // .MapEndpoint<CreateProductEndPoint>();
        
        endpoints.MapGroup("v2/categories")
            .WithTags("Categories");
            // .MapEndpoint<CreateProductEndPoint>();
        
        
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndPoint
    {
        TEndpoint.Map(app);
        return app;
    }
}