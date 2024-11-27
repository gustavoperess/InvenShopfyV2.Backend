using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses.ExpenseDto;
using InvenShopfy.Core.Models.Expenses.ExpensePayment;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Requests.Expenses.ExpensePayment;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.ExpensePayment;

public class GetExpensePaymentByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{expenseId}", HandlerAsync)
            .WithName("ExpensePayment: Get By Id")
            .WithSummary("Get a expensepayment by it's id")
            .WithDescription("Get a expensepayment by it's id")
            .WithOrder(2)
            .Produces<Response<ExpensePaymentDto?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IExpensePaymentHandler handler,
        long expenseId)
    {
        var request = new GetExpensePaymentByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            ExpenseId = expenseId
        };

        var result = await handler.GetExpensePaymentByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}