using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Requests.People.Supplier;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Supplier;

public class GetSupplierByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Suppliers: Get By Id")
            .WithSummary("Get a Supplier")
            .WithDescription("Get a Supplier")
            .WithOrder(4)
            .Produces<Response<Core.Models.People.Supplier?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISupplierHandler handler,
        long id)
    {
        var request = new GetSupplierByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}