using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.Expense;

public class CreateExpenseEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/create-expense", HandleAsync)
        .WithName("Expenses: Create A Expense")
        .WithSummary("Create a new  Expense")
        .WithDescription("Create a new Expense")
        .WithOrder(1)
        .Produces<Response<Core.Models.Expenses.Expense.Expense?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IExpenseHandler handler,
        CreateExpenseRequest request)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Expense:Add");
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
                return TypedResults.BadRequest(new Response<Core.Models.Expenses.Expense.Expense?>(null, 400, i));
            }

        }

        var result = await handler.CreateExpenseAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
}