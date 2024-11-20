using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Messages;
using InvenShopfy.Core.Models.Messages;
using InvenShopfy.Core.Requests.Messages;
using InvenShopfy.Core.Responses;


namespace InvenShopfy.API.EndPoints.Messages;

public class GetLastFiveInboxMessageEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/lastfive-messages-inbox", HandlerAsync)
            .WithName("Message: five last messages")
            .WithSummary("Get five last messages from inbox")
            .WithDescription("Get five last messages from inbox")
            .WithOrder(11)
            .Produces<Response<List<MessageDto>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IMessageHandler handler)
    {
        var request = new GetAllMessagesRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
        };

        var result = await handler.GetLastFiveInboxMessageAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}