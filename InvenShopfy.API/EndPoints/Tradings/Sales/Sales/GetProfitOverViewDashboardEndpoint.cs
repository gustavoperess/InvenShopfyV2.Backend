using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales.Dto;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Sales.Sales;

public class GetProfitOverViewDashboardEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/dashboard/profitoverview", HandlerAsync)
            .WithName("Sales: get the profitoverview for the dasboard graph")
            .WithSummary("Sales: get the profitoverview for the dasboard graph")
            .WithDescription("Sales: get the profitoverview for the dasboard graph")
            .WithOrder(11)
            .Produces<Response<List<ProfitDashBoard>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesHandler handler)
    {
        
        var result = await handler.GetProfitOverViewDashboard();
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}