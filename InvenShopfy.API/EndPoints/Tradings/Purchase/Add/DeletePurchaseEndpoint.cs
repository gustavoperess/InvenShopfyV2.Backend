using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Requests.Tradings.Purchase.Add;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Purchase.Add;

public class DeletePurchaseEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Purchases: Delete")
            .WithSummary("Delete a Purchase")
            .WithDescription("Delete a Purchase")
            .WithOrder(3)
            .Produces<Response<Core.Models.Tradings.Purchase.AddPurchase?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IPurchaseHandler handler,
        long id)
    {
        var request = new DeletePurchaseRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
