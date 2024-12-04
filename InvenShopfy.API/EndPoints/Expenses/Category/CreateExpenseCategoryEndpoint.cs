using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Models.Expenses.ExpenseCategory;
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
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:ExpenseCategory:Add");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        request.UserHasPermission = hasPermission;
        request.UserId = user.Identity?.Name ?? string.Empty;
        
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

        var result = await handler.CreateExpenseCategoryAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
}
