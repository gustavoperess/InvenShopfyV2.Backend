using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Models.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Models.Tradings.Returns.PurchaseReturn.Dto;
using InvenShopfy.Core.Requests.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Returns.PurchaseReturn;

public class GetPurchaseByReturnNumberEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/by-return/{referenceNumber}", HandlerAsync)
            .WithName("PurchaseReturn: Get By PurchaseReturn")
            .WithSummary("Get a PurchaseReturn by its PurchaseReturn")
            .WithDescription("Get a PurchaseReturn by its PurchaseReturn")
            .WithOrder(2)
            .Produces<Response<PurchaseReturnByReturnNumber?>>();

    private static async Task<IResult> HandlerAsync(
        string referenceNumber,
        ClaimsPrincipal user,
        IPurchaseReturnHandler handler)
    {
        var request = new GetPurchaseReturnByNumberRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            ReferenceNumber = referenceNumber,
        };

        var result = await handler.GetPurchasePartialByReferenceNumberAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}