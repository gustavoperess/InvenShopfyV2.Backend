using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Handlers.Warehouse;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Warehouses;

public class GetTotalStockQuantityEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("dashboard/total-in-stock", HandleAsync)
        .WithName("Warehouses: Gets the total stock quantity")
        .WithSummary("Warehouses Gets the total stock quantity")
        .WithDescription("This endpoint retrive total amount of items in stock")
        .WithOrder(5)
        .Produces<Response<int?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IWarehouseHandler handler)
    {

        var request = new GetAllWarehousesRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
        };

        var result = await handler.GetTotalInStockAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data}", result)
            : TypedResults.BadRequest(result);
    }
}