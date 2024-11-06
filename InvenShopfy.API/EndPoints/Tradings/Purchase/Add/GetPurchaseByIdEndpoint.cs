using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Models.Tradings.Purchase.Dto;
using InvenShopfy.Core.Requests.Tradings.Purchase.AddPurchase;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Purchase.Add;

public class GetPurchaseByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{purchaseId}", HandlerAsync)
            .WithName("Purchases: Get By Id")
            .WithSummary("Get a Purchase")
            .WithDescription("Get a Purchase")
            .WithOrder(4)
            .Produces<PagedResponse<PurchasePerProduct?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IPurchaseHandler handler,
        long purchaseId)
    {
        var request = new GetPurchaseByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PurchaseId = purchaseId
        };

        var result = await handler.GetPurchaseByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
