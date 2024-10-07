using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Expenses;
using InvenShopfy.Core.Requests.Expenses.Expense;
using InvenShopfy.Core.Requests.Expenses.ExpenseCategory;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Expenses.Expense;

public class UpdateExpenseEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Expenses: Update")
            .WithSummary("Update a Expense")
            .WithDescription("Update a Expense")
            .WithOrder(2)
            .Produces<Response<Core.Models.Expenses.Expense?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IExpenseHandler handler,
        UpdateExpenseRequest request,
        long id)
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
                return TypedResults.BadRequest(new Response<Core.Models.Expenses.Expense?>(null, 400, i));
            }

        }
        
        
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}