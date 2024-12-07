using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Purchase.Dto;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Purchase.Add;

public class GetLossOverViewDashboardEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/dashboard/lossoverview", HandlerAsync)
            .WithName("Purchases: get lossoverview for the dasboard graph")
            .WithSummary("Purchases: get lossoverview for the dasboard graph")
            .WithDescription("Purchases: get  lossoverview for the dasboard graph")
            .WithOrder(7)
            .Produces<Response<List<LossDashBoard>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IPurchaseHandler handler)
    {
        var result = await handler.GetLossOverViewDashboard();
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}