using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses.Dto;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Expenses.Expense;

public class GetExpenseByPartialNumberEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/expense-by/{expensenumber}", HandlerAsync)
            .WithName("Expenses: Get Expense by partial number")
            .WithSummary("Get expenses by its partial number")
            .WithDescription("Get expenses by its partial number")
            .WithOrder(8)
            .Produces<PagedResponse<ExpenseDto?>>();

    private static async Task<IResult> HandlerAsync(
        string expensenumber,
        ClaimsPrincipal user,
        IExpenseHandler handler,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetExpenseByNumberRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            ExpenseNumber = expensenumber,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetExpenseByPartialNumberAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}