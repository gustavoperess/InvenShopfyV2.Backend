using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Brand;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Brands;

public class CreateBrandEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Brands: Create Product")
        .WithSummary("Create a new brand")
        .WithDescription("Create a new brand")
        .WithOrder(1)
        .Produces<Response<Brand?>>();

    private static async Task<IResult> HandleAsync(
        IBrandHandler handler,
        CreateBrandRequest request)
    {
        request.UserId = "Test@gmail.com";
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result.Data);
        
    }
}