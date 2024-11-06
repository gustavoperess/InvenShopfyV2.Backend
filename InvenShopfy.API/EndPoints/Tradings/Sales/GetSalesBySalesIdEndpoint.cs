using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales.Dto;
using InvenShopfy.Core.Requests.Tradings.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Sales;

public class GetSalesBySalesIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/GetBySalesId/{SaleId}", HandlerAsync)
            .WithName("Get by SalesID")
            .WithSummary("Get the sales by sales Id")
            .WithDescription("Get the sales by sales Id")
            .WithOrder(10)
            .Produces<Response<SalePerProduct?>>();

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

        var result = await handler.GetSalesBySaleIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}