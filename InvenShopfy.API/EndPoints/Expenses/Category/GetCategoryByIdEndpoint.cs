using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.Category;

public class GetCategoryByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Get a Category")
            .WithDescription("Get a Category")
            .WithOrder(4)
            .Produces<Response<ExpenseCategory?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IExpenseCategoryHandler handler,
        long id)
    {
        var request = new GetExpenseCategoryByIdRequest
        {
            // UserId = user.Identity?.Name ?? string.Empty,
            UserId = "Test@gmail.com",
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}