using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Requests.People.Supplier;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Supplier;

public class CreateSupplierEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Suppliers: Create Brand")
        .WithSummary("Create a new Supplier")
        .WithDescription("Create a new Supplier")
        .WithOrder(1)
        .Produces<Response<Core.Models.People.Supplier?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISupplierHandler handler,
        CreateSupplierRequest request)
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
                return TypedResults.BadRequest(new Response<Core.Models.People.Supplier?>(null, 400, i));
            }

        }
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateSupplierAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
}