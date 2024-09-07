using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Handlers.Product;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Customer;

public class UpdateCustomerEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Customers: Update")
            .WithSummary("Update a Customer")
            .WithDescription("Update a Customer")
            .WithOrder(2)
            .Produces<Response<Core.Models.People.Customer?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        ICustomerHandler handler,
        UpdateCustomerRequest request,
        long id)
    {
        // request.UserId = user.Identity?.Name ?? string.Empty;
        request.UserId = "Test@gmail.com";
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}