using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Unit;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Products.Units;

public class DeleteUnitEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Units: Delete")
            .WithSummary("Delete a Unit")
            .WithDescription("Delete a Unit")
            .WithOrder(3)
            .Produces<Response<Unit?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IUnitHandler handler,
        long id)
    {
        var request = new DeleteUnitRequest
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