using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Messages;
using InvenShopfy.Core.Models.Messages;
using InvenShopfy.Core.Requests.Messages;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Messages;

public class ControlImportantMessageEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/important-message/{id}", HandlerAsync)
            .WithName("Message: moves to important")
            .WithSummary("Update the current message important status")
            .WithDescription("Update the current message important status")
            .WithOrder(9)
            .Produces<Response<MessageDto>?>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IMessageHandler handler,  
        [FromBody] MoveMessageRequest request,
        long id)
    {
        request.Id = id;
        var result = await handler.MoveMessageToImportantAsycn(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}