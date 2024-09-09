using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
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
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
}