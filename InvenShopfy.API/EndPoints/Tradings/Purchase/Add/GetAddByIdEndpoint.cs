using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Requests.Products.Category;
using InvenShopfy.Core.Requests.Tradings.Purchase.Add;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Purchase.Add;

public class GetAddByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Purchases: Get By Id")
            .WithSummary("Get a Purchase")
            .WithDescription("Get a Purchase")
            .WithOrder(4)
            .Produces<Response<Core.Models.Tradings.Purchase.Add?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IAddHandler handler,
        long id)
    {
        var request = new GetPurchaseByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
