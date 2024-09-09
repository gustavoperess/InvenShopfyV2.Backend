using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Requests.People.Supplier;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Supplier;

public class CreateSupplierEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Suppliers: Create Brand")
        .WithSummary("Create a new Supplier")
        .WithDescription("Create a new Supplier")
        .WithOrder(1)
        .Produces<Response<Core.Models.People.Supplier?>>();

    private static async Task<IResult> HandleAsync(
        ISupplierHandler handler,
        CreateSupplierRequest request)
    {
        request.UserId = "Test@gmail.com";
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
        
    }
}