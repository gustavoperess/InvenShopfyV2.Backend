using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Models.Tradings.Purchase.Dto;
using InvenShopfy.Core.Requests.Tradings.Purchase.AddPurchase;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Purchase.Add;

public class GetPurchaseDashboardEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("dashboard/top-purchases", HandlerAsync)
            .WithName("Purchases: Get 10 purchases for the dashboard")
            .WithSummary("Get 10 purchases for the dashboard")
            .WithDescription("Get 10 purchases for the dashboard")
            .WithOrder(6)
            .Produces<Response<List<PurchaseDashboard>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IPurchaseHandler handler)
    {
        var request = new GetAllPurchasesRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
        };

        var result = await handler.GetPurchaseStatusDashboardAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}