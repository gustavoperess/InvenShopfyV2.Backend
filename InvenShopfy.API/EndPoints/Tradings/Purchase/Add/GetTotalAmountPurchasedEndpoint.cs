using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Purchase.Add;

public class GetTotalAmountPurchasedEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("dashboard/total-amount", HandlerAsync)
            .WithName("Purchases: Get the total amount purchased")
            .WithSummary("Get the total amount purchased of all time")
            .WithDescription("Get the total amount purchased of all time")
            .WithOrder(7)
            .Produces<Response<decimal>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IPurchaseHandler handler)
    {
        var result = await handler.GetTotalPurchasedAmountAsync();
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}