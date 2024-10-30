using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Requests.Products.Product;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Products.Product;

public class UpdateProductEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Products: Update")
            .WithSummary("Update a product")
            .WithDescription("Update a product")
            .WithOrder(2)
            .Produces<Response<Core.Models.Product.Product?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IProductHandler handler,
        UpdateProductRequest request,
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
                return TypedResults.BadRequest(new Response<Core.Models.Product.Product?>(null, 400, i));
            }

        }
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
        var result = await handler.UpdateProductAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}