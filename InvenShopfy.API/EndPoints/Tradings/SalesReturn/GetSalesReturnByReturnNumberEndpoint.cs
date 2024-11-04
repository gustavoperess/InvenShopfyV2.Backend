using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.Tradings.SalesReturn;
using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Requests.Tradings.SalesReturn;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Tradings.SalesReturn;

public class GetSalesReturnByReturnNumberEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/by-return/{referenceNumber}", HandlerAsync)
            .WithName("SalesReturn: Get By returnNumber")
            .WithSummary("Get a SalesReturn by its returnNumber")
            .WithDescription("Get a SalesReturn by its returnNumber")
            .WithOrder(2)
            .Produces<Response<Core.Models.Tradings.SalesReturn.SalesReturnByReturnNumber?>>();

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