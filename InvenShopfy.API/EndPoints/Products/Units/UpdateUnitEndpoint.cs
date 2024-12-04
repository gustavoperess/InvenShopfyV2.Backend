using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Models.Product;
using InvenShopfy.Core.Requests.Products.Unit;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Products.Units;

public class UpdateUnitEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Units: Update")
            .WithSummary("Update a Unit")
            .WithDescription("Update a Unit")
            .WithOrder(2)
            .Produces<Response<Unit?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IUnitHandler handler,
        UpdateUnitRequest request,
        long id)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:ProductUnit:Update");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
        request.UserHasPermission = hasPermission;
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
      
        var result = await handler.UpdateProductUnitAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}