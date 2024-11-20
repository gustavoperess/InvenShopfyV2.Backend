using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Messages;
using InvenShopfy.Core.Requests.Messages;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Messages;

public class GetCountTrashEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/trash-messages-amount", HandlerAsync)
            .WithName("Message: Get the total amount for the messages in the trash")
            .WithSummary("Get the total amount for the messagess in the trash")
            .WithDescription("Get the total amount for the messages in the trash")
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

        var result = await handler.CountTrashtMessagesAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}