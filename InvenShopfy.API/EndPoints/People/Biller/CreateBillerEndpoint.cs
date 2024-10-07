using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Requests.People.Biller;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Biller;

public class CreateBillerEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Biller: Create Brand")
        .WithSummary("Create a new Biller")
        .WithDescription("Create a new Biller")
        .WithOrder(1)
        .Produces<Response<Core.Models.People.Biller?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IBillerHandler handler,
        CreateBillerRequest request)
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
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
}