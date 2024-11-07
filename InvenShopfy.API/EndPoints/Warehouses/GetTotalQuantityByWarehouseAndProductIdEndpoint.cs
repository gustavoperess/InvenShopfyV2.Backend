using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Warehouse;
using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Warehouses;

public class GetTotalQuantityByWarehouseAndProductIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/getTotalamount-{warehouseId}-{productId}", HandlerAsync)
            .WithName("Warehouses: Get total quantity of items")
            .WithSummary("Get total quantity of items")
            .WithDescription("Get total quantity of items")
            .WithOrder(7)
            .Produces<Response<Core.Models.Warehouse.Dto.WarehouseProductDto?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IWarehouseHandler handler,
        long productId,
        long warehouseId
        )
    {
        var request = new GetTotalQuantityByWarehouseAndProductIdRequest
        {
            WarehouseId = warehouseId,
            ProductId = productId,
        };

        var result = await handler.GetTotalQuantityByWarehouseAndProductIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}