using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.Category;

public class DeleteCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Category: Delete")
            .WithSummary("Delete a Category")
            .WithDescription("Delete a Category")
            .WithOrder(3)
            .Produces<Response<ExpenseCategory?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IExpenseCategoryHandler handler,
        long id)
    {
        var request = new DeleteExpenseCategoryRequest
        {
            // UserId = user.Identity?.Name ?? string.Empty,
            UserId = "Test@gmail.com",
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}