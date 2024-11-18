using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Notifications;
using InvenShopfy.Core.Models.Notifications;
using InvenShopfy.Core.Requests.Notifications;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Notifications;

public class GetAllNotificationsEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/notifications", HandlerAsync)
            .WithName("Notifications: Get all notifications")
            .WithSummary("Get all notifications")
            .WithDescription("Get all notifications")
            .WithOrder(1)
            .Produces<Response<List<Notification>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        INotificationHandler handler)
    {
        var request = new GetNotificationRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
        };

        var result = await handler.GetNotificationAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}