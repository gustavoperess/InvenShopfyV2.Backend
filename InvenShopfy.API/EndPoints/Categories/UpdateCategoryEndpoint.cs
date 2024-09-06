using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Category;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Categories;

public class UpdateCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Categories: Update")
            .WithSummary("Update a Category")
            .WithDescription("Update a Category")
            .WithOrder(2)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        ICategoryHandler handler,
        UpdateCategoryRequest request,
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