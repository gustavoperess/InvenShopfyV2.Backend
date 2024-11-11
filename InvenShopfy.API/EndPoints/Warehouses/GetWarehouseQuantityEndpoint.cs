using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Handlers.Warehouse;
using InvenShopfy.Core.Requests.Tradings.Sales;
using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Warehouses;

public class GetWarehouseQuantityEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("dashboard/warehouses-quantity", HandlerAsync)
            .WithName("Warehouse: Get the total amount for all warehouses")
            .WithSummary("Get total amount for  warehouses")
            .WithDescription("Get total amount for  warehouses")
            .WithOrder(6)
            .Produces<Response<int?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IWarehouseHandler handler)
    {
        var request = new GetWarehouseQuantityRequest
        {
            UserId = user.Identity?.Name ?? string.Empty
        };

        var result = await handler.GetWarehouseQuantityAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}