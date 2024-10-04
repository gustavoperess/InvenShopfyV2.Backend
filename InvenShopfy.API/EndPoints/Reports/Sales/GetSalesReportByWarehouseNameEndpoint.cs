using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.Reports.Sales;
using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Requests.Reports.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Reports.Sales;

public class GetSalesReportByWarehouseNameEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{warehouse}", HandlerAsync)
            .WithName("SalesReport: Get warehouses by name")
            .WithSummary("Get all warehouses by name")
            .WithDescription("Get all warehouses by name")
            .WithOrder(2)
            .Produces<PagedResponse<Core.Models.Reports.SaleReport?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesReportHandler handler,
        string warehouse)
    {
        var request = new GetSalesReportByWarehouseRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            WarehouseName = warehouse
  
        };

        var result = await handler.GetByWarehouseNameAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}