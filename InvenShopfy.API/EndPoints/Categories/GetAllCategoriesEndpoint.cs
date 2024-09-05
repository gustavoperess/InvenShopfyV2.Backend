using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Category;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Categories;

public class GetAllCategoriesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Categories: Get Category")
            .WithSummary("Get All Category")
            .WithDescription("Get all Category")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        ICategoryHandler handler,
        [FromQuery]DateTime? startDate=null,
        [FromQuery]DateTime? endDate=null,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllCategoriesRequest()
        {
            // UserId = user.Identity?.Name ?? string.Empty,
            UserId = "Test@gmail.com",
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}