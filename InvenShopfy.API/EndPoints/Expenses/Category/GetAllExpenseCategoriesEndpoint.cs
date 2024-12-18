using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Models.Expenses.ExpenseCategory;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Expenses.Category;

public class GetAllExpenseCategoriesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Categories: Gets all Categories expenses")
            .WithSummary("Gets all Categories expenses")
            .WithDescription("Gets all Categories expenses")
            .WithOrder(5)
            .Produces<PagedResponse<List<ExpenseCategory>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IExpenseCategoryHandler handler,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:ExpenseCategory:View");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        var request = new GetAllExpensesCategoriesRequest
        {
            UserHasPermission = hasPermission,
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetExpenseCategoryByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}