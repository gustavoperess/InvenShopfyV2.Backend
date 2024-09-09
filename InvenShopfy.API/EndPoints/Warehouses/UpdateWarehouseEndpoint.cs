using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Handlers.Warehouse;
using InvenShopfy.Core.Requests.UserManagement.User;
using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Warehouses;

public class UpdateWarehouseEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Warehouses: Update")
            .WithSummary("Update a Warehouse")
            .WithDescription("Update a Warehouse")
            .WithOrder(2)
            .Produces<Response<Core.Models.Warehouse.Warehouse?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IWarehouseHandler handler,
        UpdateWarehouseRequest request,
        long id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}