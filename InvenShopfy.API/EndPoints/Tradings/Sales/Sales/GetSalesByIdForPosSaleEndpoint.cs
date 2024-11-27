using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales.Dto;
using InvenShopfy.Core.Requests.Tradings.Sales.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Sales.Sales;

public class GetSalesByIdForPosSaleEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/posSale/{SaleId}", HandlerAsync)
            .WithName("Get by SalesID for PosSale")
            .WithSummary("Get the sales by sales Id for the posSale API")
            .WithDescription("Get the sales by sales Id for the posSale API")
            .WithOrder(11)
            .Produces<Response<PosSale?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesHandler handler,
        long saleId)
    {
        var request = new GetSalesBySaleIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            SaleId = saleId
        };

        var result = await handler.GetSalesBySaleIdForPosSaleAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}