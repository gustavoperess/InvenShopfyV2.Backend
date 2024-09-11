using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Requests.Tradings.Purchase.Add;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Purchase.Add;

public class UpdatePurchaseEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Purchases: Update")
            .WithSummary("Update a Purchase")
            .WithDescription("Update a Purchase")
            .WithOrder(2)
            .Produces<Response<Core.Models.Tradings.Purchase.AddPurchase?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IPurchaseHandler handler,
        UpdatePurchaseRequest request,
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