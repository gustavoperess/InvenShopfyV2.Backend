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
        [FromServices] RoleManager<CustomIdentityRole> roleManager,
        [FromServices] UserManager<CustomUserRequest> userManager,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        // var roleNamesOne = user.Claims
        //     .Where(c => c.Type == ClaimTypes.Role)
        //     .Select(c => c.Value)
        //     .ToList();
        // var currentUser = await userManager.FindByNameAsync(user.Identity?.Name ?? string.Empty);
        // if (currentUser == null)
        // {
        //     return TypedResults.BadRequest(new { Message = "User not found." });
        // }
        //
        // var roleNames = await userManager.GetRolesAsync(currentUser);
        // var roleIds = new List<long>();
        // foreach (var roleName in roleNames)
        // {
        //     var role = await roleManager.FindByNameAsync(roleName);
        //     if (role != null)
        //     {
        //         roleIds.Add(role.Id);
        //     }
        // }
        var request = new GetAllBrandsRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            // RoleIds = roleIds, 
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        
        var result = await handler.GetProductBrandByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}