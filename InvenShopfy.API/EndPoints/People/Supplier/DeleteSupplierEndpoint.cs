using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Requests.People.Supplier;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Supplier;

public class DeleteSupplierEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Suppliers: Delete")
            .WithSummary("Delete a Supplier")
            .WithDescription("Delete a Supplier")
            .WithOrder(3)
            .Produces<Response<Core.Models.People.Supplier?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        ISupplierHandler handler,
        long id)
    {
        var request = new DeleteSupplierRequest
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