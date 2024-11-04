using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Requests.Tradings.Purchase.AddPurchase;
using InvenShopfy.Core.Requests.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Returns.PurchaseReturn;

public class DeletePurchaseReturnEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("PurchaseReturn: Delete")
            .WithSummary("Delete a PurchaseReturn")
            .WithDescription("Delete a PurchaseReturn")
            .WithOrder(4)
            .Produces<Response<Core.Models.Tradings.Returns.PurchaseReturn.PurchaseReturn?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IPurchaseReturnHandler handler,
        long id)
    {
        var request = new DeletePurchaseReturnRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.DeletePurchaseReturnAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}