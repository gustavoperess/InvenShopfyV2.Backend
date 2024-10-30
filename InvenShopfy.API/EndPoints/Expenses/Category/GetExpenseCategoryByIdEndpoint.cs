using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.Category;

public class GetExpenseCategoryByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Categories: Gets a Category Expense By Id")
            .WithSummary("Get a Category expense by Id")
            .WithDescription("Get a Category expense by Id")
            .WithOrder(4)
            .Produces<Response<ExpenseCategory?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IExpenseCategoryHandler handler,
        long id)
    {
        var request = new GetExpenseCategoryByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetExpenseCategoryByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}