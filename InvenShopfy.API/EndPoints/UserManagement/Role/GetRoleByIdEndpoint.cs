using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Requests.UserManagement.Role;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.UserManagement.Role;

public class GetRoleByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Roles: Get By Id")
            .WithSummary("Get a Role")
            .WithDescription("Get a Role")
            .WithOrder(4)
            .Produces<Response<Core.Models.UserManagement.Role?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IUserManagementRoleHandler handler,
        long id)
    {
        var request = new GetRoleByIdRequest
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