using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Category;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Categories;

public class DeleteCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Categories: Delete")
            .WithSummary("Delete a Category")
            .WithDescription("Delete a Category")
            .WithOrder(3)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        ICategoryHandler handler,
        long id)
    {
        var request = new DeleteCategoryRequest
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