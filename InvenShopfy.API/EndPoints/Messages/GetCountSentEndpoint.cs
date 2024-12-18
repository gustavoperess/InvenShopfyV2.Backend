using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Messages;
using InvenShopfy.Core.Requests.Messages;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Messages;

public class GetCountSentEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/sent-messages-amount", HandlerAsync)
            .WithName("Message: Get the total amount for the sent messages")
            .WithSummary("Get the total amount for the sent messagess")
            .WithDescription("Get the total amount for the sent messages")
            .WithOrder(5)
            .Produces<Response<int?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IMessageHandler handler)
    {
        var request = new GetAllMessagesRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
        };

        var result = await handler.CountSentMessageAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}