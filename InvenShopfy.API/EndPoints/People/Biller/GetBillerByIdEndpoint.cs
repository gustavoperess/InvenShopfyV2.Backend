using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Requests.People.Biller;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Biller;

public class GetBillerByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Biller: Get By Id")
            .WithSummary("Get a Biller")
            .WithDescription("Get a Biller")
            .WithOrder(4)
            .Produces<Response<Core.Models.People.Biller?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IBillerHandler handler,
        long id)
    {
        var request = new GetBillerByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetBillerByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}