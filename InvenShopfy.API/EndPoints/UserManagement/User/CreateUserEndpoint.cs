using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Requests.UserManagement.User;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.UserManagement.User;

public class CreateUserEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Users: Create Brand")
        .WithSummary("Create a new User")
        .WithDescription("Create a new User")
        .WithOrder(1)
        .Produces<Response<Core.Models.UserManagement.User?>>();

    private static async Task<IResult> HandleAsync(
        IUserManagementUserHandler handler,
        CreateUserRequest request)
    {
        request.UserId = "Test@gmail.com";
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result.Data);
        
    }
}