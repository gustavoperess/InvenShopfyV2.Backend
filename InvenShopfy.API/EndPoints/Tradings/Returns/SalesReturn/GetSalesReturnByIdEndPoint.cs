using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Models.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Requests.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Returns.SalesReturn;

public class GetSalesReturnByIdEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("SalesReturn: Get By Id")
            .WithSummary("Get SalesReturn")
            .WithDescription("Get SalesReturn SalesReturn")
            .WithOrder(7)
            .Produces<Response<SaleReturn?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesReturnHandler handler,
        long id)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:SalesReturn:View");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        var request = new GetSalesReturnByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id,
            UserHasPermission = hasPermission
        };

        var result = await handler.GetSalesReturnByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}