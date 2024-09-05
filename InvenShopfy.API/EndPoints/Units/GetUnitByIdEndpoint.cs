using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Category;
using InvenShopfy.Core.Requests.Unit;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Units;

public class GetUnitByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Units: Get By Id")
            .WithSummary("Get a Unit")
            .WithDescription("Get a Unit")
            .WithOrder(4)
            .Produces<Response<Unit?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IUnitHandler handler,
        long id)
    {
        var request = new GetUnitByIdRequest()
        {
            // UserId = user.Identity?.Name ?? string.Empty,
            UserId = "Test@gmail.com",
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}