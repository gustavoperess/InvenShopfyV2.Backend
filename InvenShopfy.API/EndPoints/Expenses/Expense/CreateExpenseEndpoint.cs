using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.Expense;

public class CreateExpenseEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Expenses: Create A Expense")
        .WithSummary("Create a new  Expense")
        .WithDescription("Create a new Expense")
        .WithOrder(1)
        .Produces<Response<Core.Models.Expenses.Expense?>>();

    private static async Task<IResult> HandleAsync(
        IExpenseHandler handler,
        CreateExpenseRequest request)
    {
        request.UserId = "Test@gmail.com";
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result.Data);
        
    }
}