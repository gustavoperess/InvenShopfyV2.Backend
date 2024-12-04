using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Requests.Tradings.Sales.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Sales.Sales;

public class DeleteSaleEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Sales: Delete")
            .WithSummary("Delete a sale")
            .WithDescription("Delete a sale")
            .WithOrder(3)
            .Produces<Response<Core.Models.Tradings.Sales.Sale?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesHandler handler,
        long id)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Sales:Delete");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        var request = new DeleteSalesRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id,
            UserHasPermission = hasPermission
        };

        var result = await handler.DeleteSaleAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}