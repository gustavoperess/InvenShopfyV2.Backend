using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Handlers.Warehouse;
using InvenShopfy.Core.Requests.UserManagement.User;
using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Warehouses;

public class GetWarehouseByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/single{id}", HandlerAsync)
            .WithName("Warehouses: Get By Id")
            .WithSummary("Get a Warehouse")
            .WithDescription("Get a Warehouse")
            .WithOrder(4)
            .Produces<Response<Core.Models.Warehouse.Warehouse?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IWarehouseHandler handler,
        long id)
    {
        var request = new GetWarehouseByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}