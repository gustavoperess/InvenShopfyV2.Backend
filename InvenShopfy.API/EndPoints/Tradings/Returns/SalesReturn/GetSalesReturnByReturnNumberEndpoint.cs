using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Models.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Requests.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Returns.SalesReturn;

public class GetSalesReturnByReturnNumberEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/by-return/{referenceNumber}", HandlerAsync)
            .WithName("SalesReturn: Get By returnNumber")
            .WithSummary("Get a SalesReturn by its returnNumber")
            .WithDescription("Get a SalesReturn by its returnNumber")
            .WithOrder(2)
            .Produces<Response<SalesReturnByReturnNumber?>>();

    private static async Task<IResult> HandlerAsync(
        string referenceNumber,
        ClaimsPrincipal user,
        ISalesReturnHandler handler)
    {
        var request = new GetSalesReturnByNumberRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            ReferenceNumber = referenceNumber,
        };

        var result = await handler.GetSalesPartialByReferenceNumberAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}