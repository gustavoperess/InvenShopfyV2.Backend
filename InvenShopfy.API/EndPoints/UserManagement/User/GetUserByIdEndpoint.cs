using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Requests.UserManagement.User;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.UserManagement.User;

public class GetUserByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Users: Get By Id")
            .WithSummary("Get a User")
            .WithDescription("Get a User")
            .WithOrder(4)
            .Produces<Response<Core.Models.UserManagement.User?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IUserManagementUserHandler handler,
        long id)
    {
        var request = new GetUserRequestById
        {
            // UserId = user.Identity?.Name ?? string.Empty,
            UserId = "Test@gmail.com",
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}