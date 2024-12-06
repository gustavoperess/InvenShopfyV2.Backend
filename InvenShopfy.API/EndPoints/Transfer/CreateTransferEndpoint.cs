using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Transfer;
using InvenShopfy.Core.Requests.Transfers;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Transfer;

public class CreateTransferEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/create-transfer", HandleAsync)
        .WithName("Transfers: Create transfer")
        .WithSummary("Create a new transfer")
        .WithDescription("Create a new transfer")
        .WithOrder(1)
        .Produces<Response<Core.Models.Transfer.Transfer?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITransferHandler handler,
        CreateTransferRequest request)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Transfers:Add");
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
                return TypedResults.BadRequest(new Response<Core.Models.Transfer.Transfer?>(null, 400, i));
            }

        }

        var result = await handler.CreateTransferAsyncAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}