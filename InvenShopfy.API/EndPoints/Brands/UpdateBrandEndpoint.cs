using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Brand;
using InvenShopfy.Core.Requests.Product;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Brands;

public class UpdateBrandEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Products: Update")
            .WithSummary("Update a product")
            .WithDescription("Update a product")
            .WithOrder(2)
            .Produces<Response<Brand?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IBrandHandler handler,
        UpdateBrandRequest request,
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