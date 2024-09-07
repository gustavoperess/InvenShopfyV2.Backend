using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Requests.People.Biller;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Customer;

public class CreateCustomerEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Customers: Create Brand")
        .WithSummary("Create a new Customer")
        .WithDescription("Create a new Customer")
        .WithOrder(1)
        .Produces<Response<Core.Models.People.Customer?>>();

    private static async Task<IResult> HandleAsync(
        ICustomerHandler handler,
        CreateCustomerRequest request)
    {
        request.UserId = "Test@gmail.com";
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result.Data);
        
    }
}