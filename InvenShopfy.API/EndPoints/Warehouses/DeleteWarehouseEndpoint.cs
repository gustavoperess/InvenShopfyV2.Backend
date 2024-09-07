using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Handlers.Warehouse;
using InvenShopfy.Core.Requests.UserManagement.User;
using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Warehouses;

public class DeleteWarehouseEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Warehouses: Delete")
            .WithSummary("Delete a Warehouse")
            .WithDescription("Delete a Warehouse")
            .WithOrder(3)
            .Produces<Response<Core.Models.Warehouse.Warehouse?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IWarehouseHandler handler,
        long id)
    {
        var request = new DeleteWarehouseRequest
        {
            // UserId = user.Identity?.Name ?? string.Empty,
            UserId = "Test@gmail.com",
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}