using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales.Dto;
using InvenShopfy.Core.Requests.Tradings.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Sales;

public class GetTotalProfitDashboardEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/dashboard/total-profit", HandlerAsync)
            .WithName("Sales: Get the total profit")
            .WithSummary("Get Get the total profit to display in the dashboard")
            .WithDescription("Get Get the total profit to display in the dashboard")
            .WithOrder(10)
            .Produces<Response<decimal>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesHandler handler)
    {
        var result = await handler.GetTotalProfitDashboardAsync();
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}