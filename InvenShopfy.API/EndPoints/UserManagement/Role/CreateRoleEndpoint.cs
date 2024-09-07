using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Requests.UserManagement.Role;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.UserManagement.Role;

public class CreateRoleEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Roles: Create Brand")
        .WithSummary("Create a new Role")
        .WithDescription("Create a new Role")
        .WithOrder(1)
        .Produces<Response<Core.Models.UserManagement.Role?>>();

    private static async Task<IResult> HandleAsync(
        IUserManagementRoleHandler handler,
        CreateRoleRequest request)
    {
        request.UserId = "Test@gmail.com";
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result.Data);
        
    }
}