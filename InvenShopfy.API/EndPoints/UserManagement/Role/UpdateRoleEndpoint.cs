using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Requests.UserManagement.Role;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.UserManagement.Role;

public class UpdateRoleEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Roles: Update")
            .WithSummary("Update a Role")
            .WithDescription("Update a Role")
            .WithOrder(2)
            .Produces<Response<Core.Models.UserManagement.Role?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IUserManagementRoleHandler handler,
        UpdateRoleRequest request,
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