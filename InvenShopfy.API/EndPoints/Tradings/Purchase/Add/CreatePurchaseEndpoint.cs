using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Requests.Tradings.Purchase.AddPurchase;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Purchase.Add;

public class CreatePurchaseEndpoint : IEndPoint

{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Purchases: Add a purchase")
        .WithSummary("Purchase: Add a purchase")
        .WithDescription("Purchase: Add a purchase")
        .WithOrder(1)
        .Produces<Response<Core.Models.Tradings.Purchase.AddPurchase?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IPurchaseHandler handler,
        CreatePurchaseRequest request)
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
                return TypedResults.BadRequest(new Response<Core.Models.Tradings.Purchase.AddPurchase?>(null, 400, i));
            }

        }
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreatePurchaseAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);

    }
}