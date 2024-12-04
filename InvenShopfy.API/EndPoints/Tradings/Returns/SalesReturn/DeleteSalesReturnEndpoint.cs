using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Models.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Requests.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Returns.SalesReturn;

public class DeleteSalesReturnEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("SalesReturn: Delete")
            .WithSummary("Delete a saleReturn")
            .WithDescription("Delete a saleReturn")
            .WithOrder(4)
            .Produces<Response<SaleReturn?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesReturnHandler handler,
        long id)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:SalesReturn:Delete");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        var request = new DeleteSalesReturnRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id,
            UserHasPermission = hasPermission,
        };

        var result = await handler.DeleteSalesReturnAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}