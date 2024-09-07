using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.Category;

public class CreateCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Categories: Create A Category Expense")
        .WithSummary("Create a new Category Expense")
        .WithDescription("Create a new Category Expense")
        .WithOrder(1)
        .Produces<Response<ExpenseCategory?>>();

    private static async Task<IResult> HandleAsync(
        IExpenseCategoryHandler handler,
        CreateExpenseCategoryRequest request)
    {
        request.UserId = "Test@gmail.com";
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result.Data);
        
    }
}