using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Products.Product;

public class GetProductByNameEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/products/by-name/{title}", HandlerAsync)
            .WithName("Products: Get By name")
            .WithSummary("Get a product by its name")
            .WithDescription("Get a product by its name")
            .WithOrder(6)
            .Produces<Response<Core.Models.Product.Product?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IProductHandler handler,
        string title)
    {
        var request = new GetProductByNameRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Title = title
        };

        var result = await handler.GeyByNameAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}