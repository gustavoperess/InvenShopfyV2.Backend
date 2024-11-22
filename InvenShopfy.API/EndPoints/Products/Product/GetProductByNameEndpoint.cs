using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product.Dto;
using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Products.Product;

public class GetProductByNameEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/by-name/{title}", HandlerAsync)
            .WithName("Products: Get By name")
            .WithSummary("Get a product by its name")
            .WithDescription("Get a product by its name")
            .WithOrder(6)
            .Produces<PagedResponse<ProductByName?>>();

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

        var result = await handler.GetProductByPartialNameAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}