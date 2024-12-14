using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Requests.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Returns.PurchaseReturn;

public class GetPurchaseReturnByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("PurchaseReturn: Get By Id")
            .WithSummary("Get PurchaseReturn")
            .WithDescription("Get PurchaseReturn SalesReturn")
            .WithOrder(6)
            .Produces<Response<Core.Models.Tradings.Returns.PurchaseReturn.PurchaseReturn?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IPurchaseReturnHandler handler,
        long id)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:PurchaseReturn:View");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        var request = new GetPurchaseReturnByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id,
            UserHasPermission = hasPermission
        };

        var result = await handler.GetPurchaseReturnByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}