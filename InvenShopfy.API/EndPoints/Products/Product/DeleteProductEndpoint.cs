using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Products.Product;

public class DeleteProductEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Products: Delete")
            .WithSummary("Delete a product")
            .WithDescription("Delete a product")
            .WithOrder(3)
            .Produces<Response<Core.Models.Product.Product?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IProductHandler handler,
        long id)
    {
        var request = new DeleteProductRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.DeleteProductAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}