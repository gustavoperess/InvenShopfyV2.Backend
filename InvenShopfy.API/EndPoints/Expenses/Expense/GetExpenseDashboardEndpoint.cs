using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses.Dto;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.Expense;

public class GetExpenseDashboardEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("dashboard/top-expenses", HandleAsync)
        .WithName("Expenses: Get 10 Expenses for the dashboard")
        .WithSummary("Get Get 10 Expenses for the dashboard")
        .WithDescription("Get Get 10 Expenses for the dashboard")
        .WithOrder(6)
        .Produces<Response<ExpenseDashboard?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IExpenseHandler handler)
    {
      
        var request = new GetAllExpensesRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
        };
        
        var result = await handler.GetExpenseStatusDashboardAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data}", result)
            : TypedResults.BadRequest(result);

    }
}