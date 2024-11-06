using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales.Dto;
using InvenShopfy.Core.Requests.Tradings.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Sales;

public class GetSalesBySellerEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/seller/{id}", HandlerAsync)
            .WithName("Sales: get a seller by Id")
            .WithSummary("Get a sales")
            .WithDescription("Get a sale")
            .WithOrder(7)
            .Produces<Response<BestSeller?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesHandler handler,
        long billerId)
    {
        var request = new GetSalesBySeller
        {
            UserId = user.Identity?.Name ?? string.Empty,
            BillerId = billerId
        };

        var result = await handler.GetSalesBySellerAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}