using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Unit;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Products.Units;

public class DeleteUnitEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Units: Delete")
            .WithSummary("Delete a Unit")
            .WithDescription("Delete a Unit")
            .WithOrder(3)
            .Produces<Response<Unit?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IUnitHandler handler,
        long id)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:ProductUnit:Delete");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        var request = new DeleteUnitRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id,
            UserHasPermission = hasPermission
        };

        var result = await handler.DeleteProductUnitAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}