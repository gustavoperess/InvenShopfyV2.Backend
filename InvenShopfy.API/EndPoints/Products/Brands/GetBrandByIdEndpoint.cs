using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Products.Brands;

public class GetBrandByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Brands: Get By Id")
            .WithSummary("Get a Brand")
            .WithDescription("Get a Brand")
            .WithOrder(4)
            .Produces<Response<Brand?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IBrandHandler handler,
        long id)
    {
        var request = new GetBrandByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetProductBrandByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}