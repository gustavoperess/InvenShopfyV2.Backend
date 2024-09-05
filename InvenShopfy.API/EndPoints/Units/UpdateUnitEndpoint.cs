using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Category;
using InvenShopfy.Core.Requests.Unit;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Units;

public class UpdateUnitEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Units: Update")
            .WithSummary("Update a Unit")
            .WithDescription("Update a Unit")
            .WithOrder(2)
            .Produces<Response<Unit?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IUnitHandler handler,
        UpdateUnitRequest request,
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