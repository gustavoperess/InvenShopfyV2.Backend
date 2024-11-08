using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Transfer;
using InvenShopfy.Core.Requests.Transfers;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Transfer;

public class GetAllTransfersEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/alltransfer", HandlerAsync)
            .WithName("Transfers: Get transfers")
            .WithSummary("Get All transfers")
            .WithDescription("Get all transfers")
            .WithOrder(2)
            .Produces<PagedResponse<List<Core.Models.Transfer.Dto.TransferDto>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ITransferHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllTransfersRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetAllTransfersAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}