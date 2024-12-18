using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Handlers.Warehouse;
using InvenShopfy.Core.Requests.UserManagement.User;
using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Warehouses;

public class GetAllWarehousesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/allwarehouses", HandlerAsync)
            .WithName("Warehouses: Get All")
            .WithSummary("Get All Warehouses")
            .WithDescription("Get all Warehouses")
            .WithOrder(5)
            .Produces<PagedResponse<List<Core.Models.Warehouse.Warehouse>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IWarehouseHandler handler,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Warehouse:View");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";

        
        var request = new GetAllWarehousesRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
            UserHasPermission = hasPermission
        };

        var result = await handler.GetWarehouseByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}