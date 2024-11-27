using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Requests.Tradings.Sales.SalesPayment;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Sales.SalesPayment;

public class CreateSalesPaymentEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("SalesPayment: Create a new salespayment")
        .WithSummary("Create a new payment for the Salespayment")
        .WithDescription("Create a new payment for the Salespayment")
        .WithOrder(1)
        .Produces<Response<Core.Models.Tradings.Sales.SalesPayment.SalesPayment?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISalesPaymentHandler handler,
        CreateSalesPaymentRequest request)
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
                return TypedResults.BadRequest(new Response<Core.Models.Tradings.Sales.SalesPayment.SalesPayment?>(null, 400, i));
            }

        }

        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateSalesPaymentAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
}