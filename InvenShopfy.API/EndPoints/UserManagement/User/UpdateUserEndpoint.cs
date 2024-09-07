using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Requests.UserManagement.User;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.UserManagement.User;

public class UpdateUserEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Users: Update")
            .WithSummary("Update a User")
            .WithDescription("Update a User")
            .WithOrder(2)
            .Produces<Response<Core.Models.UserManagement.User?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IUserManagementUserHandler handler,
        UpdateUserRequest request,
        long id)
    {
        // request.UserId = user.Identity?.Name ?? string.Empty;
        request.UserId = "Test@gmail.com";
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}