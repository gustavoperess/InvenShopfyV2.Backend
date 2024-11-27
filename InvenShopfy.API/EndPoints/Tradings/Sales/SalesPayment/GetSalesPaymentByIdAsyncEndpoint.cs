using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales.SalesPayment;
using InvenShopfy.Core.Requests.Tradings.Sales.SalesPayment;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Sales.SalesPayment;

public class GetSalesPaymentByIdAsyncEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("SalesPayment: Get By Id")
            .WithSummary("Get a SalesPayment by it's id")
            .WithDescription("Get a SalesPayment by it's id")
            .WithOrder(2)
            .Produces<Response<SalesPaymentDto?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesPaymentHandler handler,
        long id)
    {
        var request = new GetSalesPaymentByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetSalesPaymentByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}