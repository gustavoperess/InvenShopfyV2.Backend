using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Handlers.Warehouse;
using InvenShopfy.Core.Requests.UserManagement.User;
using InvenShopfy.Core.Requests.Warehouse;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Warehouses;

public class CreateWarehouseEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Warehouses: Create Brand")
        .WithSummary("Create a new Warehouse")
        .WithDescription("Create a new Warehouse")
        .WithOrder(1)
        .Produces<Response<Core.Models.Warehouse.Warehouse?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IWarehouseHandler handler,
        CreateWarehouseRequest request)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Warehouse:Add");
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
                return TypedResults.BadRequest(new Response<Core.Models.Warehouse.Warehouse?>(null, 400, i));
            }

        }
        
        var result = await handler.CreateWarehouseAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
}