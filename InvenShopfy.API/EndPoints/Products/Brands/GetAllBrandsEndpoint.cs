using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Products.Brands;

public class GetAllBrandsEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Brands: Get All")
            .WithSummary("Get All Brands")
            .WithDescription("Get all Brands")
            .WithOrder(5)
            .Produces<PagedResponse<List<Brand>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IBrandHandler handler,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:ProductBrand:View");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        if (!hasPermission)
        {
            return TypedResults.BadRequest("User does not have authorization to proceed with this task");
        }
     
        var request = new GetAllBrandsRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        
        var result = await handler.GetProductBrandByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}