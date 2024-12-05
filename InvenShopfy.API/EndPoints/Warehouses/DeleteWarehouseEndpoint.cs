using System.Security.Claims;
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
        ClaimsPrincipal user,
        IWarehouseHandler handler,
        long id)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Warehouse:Delete");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";

        
        var request = new DeleteWarehouseRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id,
            UserHasPermission = hasPermission
        };

        var result = await handler.DeleteWarehouseAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}