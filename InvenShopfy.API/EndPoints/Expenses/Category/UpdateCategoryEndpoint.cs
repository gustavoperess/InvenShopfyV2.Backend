using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.Category;

public class UpdateCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Category: Update")
            .WithSummary("Update a Category")
            .WithDescription("Update a Category")
            .WithOrder(2)
            .Produces<Response<ExpenseCategory?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IExpenseCategoryHandler handler,
        UpdateExpenseCategoryRequest request,
        long id)
    {
        // request.UserId = user.Identity?.Name ?? string.Empty;
        request.UserId = "Test@gmail.com";
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}