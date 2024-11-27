using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales.SalesPayment;
using InvenShopfy.Core.Requests.Tradings.Sales.SalesPayment;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Sales.SalesPayment;

public class GetSalesPaymentDetailsByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("view-payment/{id}", HandlerAsync)
            .WithName("SalesPayment: get paid item by id")
            .WithSummary("Get  paiditem by it's id")
            .WithDescription("Get  paiditem by it's id")
            .WithOrder(3)
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

        var result = await handler.GetSalesPaymentDetailsByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}