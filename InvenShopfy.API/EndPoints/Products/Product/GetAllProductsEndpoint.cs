using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product.Dto;
using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Products.Product;

public class GetAllProductsEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Products: Get All")
            .WithSummary("Get All products")
            .WithDescription("Get all products")
            .WithOrder(5)
            .Produces<PagedResponse<List<ProductList>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IProductHandler handler,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Product:View");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        var request = new GetAllProductsRequest
        {
            UserHasPermission = hasPermission,
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
          
        };

        var result = await handler.GetProductByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}