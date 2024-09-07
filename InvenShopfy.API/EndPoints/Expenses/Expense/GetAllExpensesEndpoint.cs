using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Expenses.Expense;

public class GetAllExpensesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Expenses: Get All")
            .WithSummary("Get All Expenses")
            .WithDescription("Get all Expenses")
            .WithOrder(5)
            .Produces<PagedResponse<List<Core.Models.Expenses.Expense>?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IExpenseHandler handler,
        [FromQuery]DateTime? startDate=null,
        [FromQuery]DateTime? endDate=null,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllExpensesRequest
        {
            // UserId = user.Identity?.Name ?? string.Empty,
            UserId = "Test@gmail.com",
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}