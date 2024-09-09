using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Handlers.Warehouse;
using InvenShopfy.Core.Requests.UserManagement.User;
using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Warehouses;

public class CreateWarehouseEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Warehouses: Create Brand")
        .WithSummary("Create a new Warehouse")
        .WithDescription("Create a new Warehouse")
        .WithOrder(1)
        .Produces<Response<Core.Models.Warehouse.Warehouse?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IWarehouseHandler handler,
        CreateWarehouseRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
}