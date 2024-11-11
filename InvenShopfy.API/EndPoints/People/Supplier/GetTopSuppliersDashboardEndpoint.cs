using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Requests.People.Supplier;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.People.Supplier;

public class GetTopSuppliersDashboardEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("dashboard/top-suppliers", HandlerAsync)
            .WithName("Suppliers: Get top 5 suppliers")
            .WithSummary("Get top 5 suppliers")
            .WithDescription("Get top 5 Suppliers")
            .WithOrder(5)
            .Produces<Response<List<Core.Models.People.Dto.TopSupplier>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISupplierHandler handler)
    {
        var request = new GetAllSuppliersRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
        };

        var result = await handler.GetTopSuppliersAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}