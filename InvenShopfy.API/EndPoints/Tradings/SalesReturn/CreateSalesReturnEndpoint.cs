using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.SalesReturn;
using InvenShopfy.Core.Requests.Tradings.SalesReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.SalesReturn;

public class CreateSalesReturnEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/create-sale", HandleAsync)
        .WithName("Create: a new salereturn")
        .WithSummary("Create a new salereturn")
        .WithDescription("This endpoint creates a new salereturn")
        .WithOrder(1)
        .Produces<Response<Core.Models.Tradings.SalesReturn.SaleReturn?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISalesReturnHandler handler,
        CreateSalesReturnRequest request)
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
                return TypedResults.BadRequest(new Response<Core.Models.Tradings.SalesReturn.SaleReturn?>(null, 400, i));
            }

        }
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateSalesReturnAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);

    }
}