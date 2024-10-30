using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.Expense;

public class GetExpenseByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Expenses: Get By Id")
            .WithSummary("Get a Expense")
            .WithDescription("Get a Expense")
            .WithOrder(4)
            .Produces<Response<Core.Models.Expenses.Expense?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IExpenseHandler handler,
        long id)
    {
        var request = new GetExpenseByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetExpenseByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}