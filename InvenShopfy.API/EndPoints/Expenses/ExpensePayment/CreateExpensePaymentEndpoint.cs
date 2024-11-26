using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.ExpensePayment;

public class CreateExpensePaymentEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("ExpensePayment: Create a new Expense Pay,ent")
        .WithSummary("Create a new payment for the Expenses")
        .WithDescription("Create a new payment for the Expenses")
        .WithOrder(1)
        .Produces<Response<Core.Models.Expenses.ExpensePayment?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IExpensePaymentHandler handler,
        CreateExpensePaymentRequest request)
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
        var result = await handler.CreateExpensePaymentAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
}