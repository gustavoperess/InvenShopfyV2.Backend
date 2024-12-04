using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Requests.Tradings.Sales.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Sales.Sales;

public class CreateSaleEndpoint  : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPost("/create-sale", HandleAsync)
            .WithName("Create: a new sale")
            .WithSummary("Create a new sale")
            .WithDescription("This endpoint creates a new sale")
            .WithOrder(1)
            .Produces<Response<Core.Models.Tradings.Sales.Sale?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ISalesHandler handler,
            CreateSalesRequest request)
        {
            var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Sales:Add");
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
                    return TypedResults.BadRequest(new Response<Core.Models.Tradings.Sales.Sale?>(null, 400, i));
                }

            }
      
            var result = await handler.CreateSaleAsync(request);
            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);

        }
}