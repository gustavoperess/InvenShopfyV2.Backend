using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Requests.Tradings.Purchase.Add;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Purchase.Add;

public class CreateAddEndpoint : IEndPoint

{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Purchases: Add a purchase")
        .WithSummary("Purchase: Add a purchase")
        .WithDescription("Purchase: Add a purchase")
        .WithOrder(1)
        .Produces<Response<Core.Models.Tradings.Purchase.Add?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IAddHandler handler,
        CreatePurchaseRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);

    }
}