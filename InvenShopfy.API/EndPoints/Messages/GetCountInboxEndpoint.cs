using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Messages;
using InvenShopfy.Core.Requests.Messages;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Messages;

public class GetCountInboxEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/inbox-messages-amount", HandlerAsync)
            .WithName("Message: Get the total amount for the inbox messages")
            .WithSummary("Get the total amount for the inbox messagess")
            .WithDescription("Get the total amount for the inbox messages")
            .WithOrder(6)
            .Produces<Response<int?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IMessageHandler handler)
    {
        var request = new GetAllMessagesRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
        };

        var result = await handler.CountInboxMessagesAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}