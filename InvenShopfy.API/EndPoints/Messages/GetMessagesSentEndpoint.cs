using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Messages;
using InvenShopfy.Core.Models.Messages;
using InvenShopfy.Core.Requests.Messages;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Messages;

public class GetMessagesSentEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/messages-sent", HandlerAsync)
            .WithName("Message: Get messages sent")
            .WithSummary("Get messages sent")
            .WithDescription("Get all messages sent")
            .WithOrder(5)
            .Produces<PagedResponse<List<MessageDto>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IMessageHandler handler,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllMessagesRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetSentMessagesAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}