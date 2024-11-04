using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Requests.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Returns.PurchaseReturn;

public class CreatePurchaseReturnEndpoin : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/create-purhasereturn", HandleAsync)
        .WithName("PurchaseReturn: a new purchaseReturn")
        .WithSummary("PurchaseReturn a new purchaseReturn")
        .WithDescription("This endpoint creates a new purchaseReturn")
        .WithOrder(1)
        .Produces<Response<Core.Models.Tradings.Returns.PurchaseReturn.PurchaseReturn?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IPurchaseReturnHandler handler,
        CreatePurchaseReturnRequest request)
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
                return TypedResults.BadRequest(new Response<Core.Models.Tradings.Returns.PurchaseReturn.PurchaseReturn?>(null, 400, i));
            }

        }
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreatePurchaseReturnAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);

    }
}