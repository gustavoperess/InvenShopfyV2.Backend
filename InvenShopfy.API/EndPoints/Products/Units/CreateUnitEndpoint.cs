using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Unit;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Products.Units;

public class CreateUnitEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Units: Create Unit")
        .WithSummary("Create a new Unit")
        .WithDescription("Create a new Unit")
        .WithOrder(1)
        .Produces<Response<Unit?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUnitHandler handler,
        CreateUnitRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
}