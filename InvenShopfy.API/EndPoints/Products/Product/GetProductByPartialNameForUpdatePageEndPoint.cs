using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product.Dto;
using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Products.Product;

public class GetProductByPartialNameForUpdatePageEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/by-name-adjustment/{title}", HandlerAsync)
            .WithName("Products: Get for update page")
            .WithSummary("Get a product by its partial name for the update page")
            .WithDescription("Get a product by its partial name for the update page")
            .WithOrder(7)
            .Produces<PagedResponse<ProductByNameForUpdatePage?>>();

    private static async Task<IResult> HandlerAsync(
        string title,
        ClaimsPrincipal user,
        IProductHandler handler,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetProductByNameRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            ProductName = title,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetProductByPartialNameForUpdatePageAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}