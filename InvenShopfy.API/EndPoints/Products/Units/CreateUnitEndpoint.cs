using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Unit;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Products.Units;

public class CreateUnitEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Units: Create Unit")
        .WithSummary("Create a new Unit")
        .WithDescription("Create a new Unit")
        .WithOrder(1)
        .Produces<Response<Unit?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUnitHandler handler,
        CreateUnitRequest request)
    {
        
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:ProductUnit:Add");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        request.UserHasPermission = hasPermission;
        request.UserId = user.Identity?.Name ?? string.Empty;
        
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(request);
        bool isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);
        
        if (!isValid)
        {
            var errors = validationResults.Select(v => v.ErrorMessage).ToList();
            foreach (var i in errors)
            {
                Console.WriteLine($"{i}");
                return TypedResults.BadRequest(new Response<Unit?>(null, 400, i));
            }

        }
        
        var result = await handler.CreateProductUnitAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
}