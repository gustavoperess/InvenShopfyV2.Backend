using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.Category;

public class CreateExpenseCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Categories: Create A Category Expense")
        .WithSummary("Create a new Category Expense")
        .WithDescription("Create a new Category Expense")
        .WithOrder(1)
        .Produces<Response<ExpenseCategory?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IExpenseCategoryHandler handler,
        CreateExpenseCategoryRequest request)
    {
        
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(request);
        bool isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);
        
        if (!isValid)
        {
            var errors = validationResults.Select(v => v.ErrorMessage).ToList();
            foreach (var i in errors)
            {
                Console.WriteLine($"{i}");
                return TypedResults.BadRequest(new Response<ExpenseCategory?>(null, 400, i));
            }

        }

        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
}
// BW1-00570