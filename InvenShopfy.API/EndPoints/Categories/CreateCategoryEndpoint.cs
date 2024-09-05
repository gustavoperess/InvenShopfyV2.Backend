using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Category;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Categories;

public class CreateCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Categories: Create Category")
        .WithSummary("Create a new Category")
        .WithDescription("Create a new Category")
        .WithOrder(1)
        .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        CreateCategoryRequest request)
    {
        request.UserId = "Test@gmail.com";
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result.Data);
        
    }
}