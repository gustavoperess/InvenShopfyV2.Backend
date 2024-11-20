using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Messages;
using InvenShopfy.Core.Requests.Messages;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Messages;

public class GetICountImportantEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/important-messages-amount", HandlerAsync)
            .WithName("Message: Get the total amount for the important messages")
            .WithSummary("Get the total amount for the important messagess")
            .WithDescription("Get the total amount for the important messages")
            .WithOrder(4)
            .Produces<Response<int?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IMessageHandler handler)
    {
        var request = new GetAllMessagesRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
        };

        var result = await handler.CountImportantMessagesAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}