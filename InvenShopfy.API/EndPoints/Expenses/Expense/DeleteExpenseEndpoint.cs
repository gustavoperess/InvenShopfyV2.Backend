using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.Expense;

public class DeleteExpenseEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Expenses: Delete")
            .WithSummary("Delete a Expense")
            .WithDescription("Delete a Expense")
            .WithOrder(3)
            .Produces<Response<Core.Models.Expenses.Expense.Expense?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IExpenseHandler handler,
        long id)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Expense:Delete");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        var request = new DeleteExpenseRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id,
            UserHasPermission = hasPermission
        };

        var result = await handler.DeleteExpenseAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}