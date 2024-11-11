using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Requests.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Returns.PurchaseReturn;

public class GetPurchaseReturnTotalAmountEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("dashboard/total-purchases-returned", HandleAsync)
        .WithName("PurchaseReturn: sales return total amount")
        .WithSummary("PurchaseReturn get sales return total amount")
        .WithDescription("This endpoint retrive sales return total amount")
        .WithOrder(5)
        .Produces<Response<decimal?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IPurchaseReturnHandler handler)
    {
      
        var request = new GetAllPurchaseReturnsRequests
        {
            UserId = user.Identity?.Name ?? string.Empty,
        };
        
        var result = await handler.GetTotalPurchaseReturnAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data}", result)
            : TypedResults.BadRequest(result);

    }
}