using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Products.Product;

public class CreateProductEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Products: Create Product")
        .WithSummary("Create a new product")
        .WithDescription("Create a new product")
        .WithOrder(1)
        .Produces<Response<Core.Models.Product.Product?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProductHandler handler,
        CreateProductRequest request)
    {
    
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
        
}