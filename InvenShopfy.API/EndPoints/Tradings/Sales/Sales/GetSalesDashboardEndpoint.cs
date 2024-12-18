using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales.Dto;
using InvenShopfy.Core.Requests.Tradings.Sales.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Sales.Sales;

public class GetSalesDashboardEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/dashboard/top-sales", HandlerAsync)
            .WithName("Sales: Get 10 sales for the dashboard")
            .WithSummary("Get Get 10 sales for the dashboard")
            .WithDescription("Get Get 10 sales for the dashboard")
            .WithOrder(9)
            .Produces<Response<List<SallerDashboard>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesHandler handler)
    {
        
        var result = await handler.GetSaleStatusDashboardAsync();
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}