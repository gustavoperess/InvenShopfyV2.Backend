using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Requests.People.Biller;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Customer;

public class DeleteCustomerEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Customers: Delete")
            .WithSummary("Delete a Customer")
            .WithDescription("Delete a Customer")
            .WithOrder(3)
            .Produces<Response<Core.Models.People.Customer?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        ICustomerHandler handler,
        long id)
    {
        var request = new DeleteCustomerRequest
        {
            // UserId = user.Identity?.Name ?? string.Empty,
            UserId = "Test@gmail.com",
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}