using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Category;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Products.Categories;

public class GetCategoryByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Get a Category")
            .WithDescription("Get a Category")
            .WithOrder(4)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ICategoryHandler handler,
        long id)
    {
        var request = new GetCategoryByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetProductCategoryByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}