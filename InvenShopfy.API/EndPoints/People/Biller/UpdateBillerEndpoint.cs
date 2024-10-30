using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Biller;

public class UpdateBillerEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Biller: Update")
            .WithSummary("Update a Biller")
            .WithDescription("Update a Biller")
            .WithOrder(2)
            .Produces<Response<Core.Models.People.Biller?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IBrandHandler handler,
        UpdateBrandRequest request,
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
                return TypedResults.BadRequest(new Response<Core.Models.People.Biller?>(null, 400, i));
            }

        }
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
        var result = await handler.UpdateProductBrandAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}