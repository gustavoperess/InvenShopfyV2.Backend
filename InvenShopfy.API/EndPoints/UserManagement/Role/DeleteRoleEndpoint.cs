using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Requests.UserManagement.Role;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.UserManagement.Role;

public class DeleteRoleEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Roles: Delete")
            .WithSummary("Delete a Role")
            .WithDescription("Delete a Role")
            .WithOrder(3)
            .Produces<Response<Core.Models.UserManagement.Role?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IUserManagementRoleHandler handler,
        long id)
    {
        var request = new DeleteRoleRequest
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