using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.Expense;

public class GetExpenseTotalAmountEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("dashboard/total-amount", HandleAsync)
        .WithName("Expenses: Gets the total amount for expenses")
        .WithSummary("Expenses Gets the total amount for expenses")
        .WithDescription("This endpoint retrive total amount for expenses")
        .WithOrder(5)
        .Produces<Response<decimal?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IExpenseHandler handler)
    {

        var request = new GetAllExpensesRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
        };

        var result = await handler.GetExpenseTotalAmountAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data}", result)
            : TypedResults.BadRequest(result);

    }
}