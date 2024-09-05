using InvenShopfy.API.Common.Api;
using InvenShopfy.API.EndPoints.Brands;
using InvenShopfy.API.EndPoints.Categories;
using InvenShopfy.API.EndPoints.Products;
using InvenShopfy.API.EndPoints.Units;

namespace InvenShopfy.API.EndPoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/").WithTags("Health Check").MapGet("/", () => new { message = "Ok" });

        endpoints.MapGroup("v2/products")
            .WithTags("Products")
            .MapEndpoint<CreateProductEndpoint>()
            .MapEndpoint<UpdateProductEndpoint>()
            .MapEndpoint<GetAllProductsEndpoints>()
            .MapEndpoint<DeleteProductEndpoint>()
            .MapEndpoint<GetProductByIdEndpoint>();
        
        endpoints.MapGroup("v2/brands")
            .WithTags("Brands")
            .MapEndpoint<CreateBrandEndpoint>()
            .MapEndpoint<UpdateBrandEndpoint>()
            .MapEndpoint<GetAllBrandsEndpoint>()
            .MapEndpoint<DeleteBrandEndpoint>()
            .MapEndpoint<GetBrandByIdEndpoint>();
        
        endpoints.MapGroup("v2/units")
            .WithTags("Units")
            .MapEndpoint<CreateUnitEndpoint>()
            .MapEndpoint<UpdateUnitEndpoint>()
            .MapEndpoint<GetAllUnitsEndpoint>()
            .MapEndpoint<DeleteUnitEndpoint>()
            .MapEndpoint<GetUnitByIdEndpoint>();
        
        endpoints.MapGroup("v2/categories")
            .WithTags("Categories")
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>();
        
        
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndPoint
    {
        TEndpoint.Map(app);
        return app;
    }
}