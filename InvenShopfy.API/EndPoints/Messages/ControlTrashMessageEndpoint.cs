using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Messages;
using InvenShopfy.Core.Models.Messages;
using InvenShopfy.Core.Requests.Messages;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Messages;

public class ControlTrashMessageEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/trash-message/{id}", HandlerAsync)
            .WithName("Message: moves to trash")
            .WithSummary("Moves the current message to trash can")
            .WithDescription("Moves the current message to trash can")
            .WithOrder(8)
            .Produces<Response<MessageDto>?>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IMessageHandler handler,
        [FromBody] MoveMessageRequest request,
        long id)
    {
        request.Id = id;
        var result = await handler.MoveMessageToTrashAsycn(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}