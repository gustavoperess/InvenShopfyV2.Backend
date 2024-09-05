using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Brand;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Brands;

public class DeleteBrandEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Brands: Delete")
            .WithSummary("Delete a brand")
            .WithDescription("Delete a brand")
            .WithOrder(3)
            .Produces<Response<Brand?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IBrandHandler handler,
        long id)
    {
        var request = new DeleteBrandRequest()
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