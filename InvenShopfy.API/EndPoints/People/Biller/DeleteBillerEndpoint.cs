using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Requests.People.Biller;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Biller;

public class DeleteBillerEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Biller: Delete")
            .WithSummary("Delete a Biller")
            .WithDescription("Delete a Biller")
            .WithOrder(3)
            .Produces<Response<Core.Models.People.Biller?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IBillerHandler handler,
        long id)
    {
        var request = new DeleteBillerRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}