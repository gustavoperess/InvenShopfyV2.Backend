using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Customer;

public class UpdateCustomerEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Customers: Update")
            .WithSummary("Update a Customer")
            .WithDescription("Update a Customer")
            .WithOrder(2)
            .Produces<Response<Core.Models.People.Customer?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ICustomerHandler handler,
        UpdateCustomerRequest request,
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
                return TypedResults.BadRequest(new Response<Core.Models.People.Customer?>(null, 400, i));
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