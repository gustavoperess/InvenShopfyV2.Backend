using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Requests.UserManagement.User;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.UserManagement.User;

public class DeleteUserEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Users: Delete")
            .WithSummary("Delete a User")
            .WithDescription("Delete a User")
            .WithOrder(3)
            .Produces<Response<Core.Models.UserManagement.User?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IUserManagementUserHandler handler,
        long id)
    {
        var request = new DeleteUserRequest
        {
            // UserId = user.Identity?.Name ?? string.Empty,
            UserId = "Test@gmail.com",
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}