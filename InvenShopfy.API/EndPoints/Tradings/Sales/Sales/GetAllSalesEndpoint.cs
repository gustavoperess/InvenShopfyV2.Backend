using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales.Dto;
using InvenShopfy.Core.Requests.Tradings.Sales.Sales;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Tradings.Sales.Sales;

public class GetAllSalesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/allsales", HandlerAsync)
            .WithName("Sales: Get sales")
            .WithSummary("Get All sales")
            .WithDescription("Get all sales")
            .WithOrder(5)
            .Produces<PagedResponse<List<SaleList>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesHandler handler,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Sales:View");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        var request = new GetAllSalesRequest
        {
            UserHasPermission = hasPermission,
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetSaleByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}