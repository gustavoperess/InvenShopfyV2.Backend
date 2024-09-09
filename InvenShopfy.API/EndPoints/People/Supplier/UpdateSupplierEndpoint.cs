using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Requests.People.Supplier;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Supplier;

public class UpdateSupplierEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Suppliers: Update")
            .WithSummary("Update a Supplier")
            .WithDescription("Update a Supplier")
            .WithOrder(2)
            .Produces<Response<Core.Models.People.Supplier?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISupplierHandler handler,
        UpdateSupplierRequest request,
        long id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}