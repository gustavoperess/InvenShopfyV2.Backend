using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Requests.People.Biller;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Customer;

public class GetCustomerByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Customers: Get By Id")
            .WithSummary("Get a Customer")
            .WithDescription("Get a Customer")
            .WithOrder(4)
            .Produces<Response<Core.Models.People.Customer?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ICustomerHandler handler,
        long id)
    {
        var request = new GetCustomerByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetCustomerByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}