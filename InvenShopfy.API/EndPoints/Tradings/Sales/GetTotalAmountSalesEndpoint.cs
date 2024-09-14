using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Requests.Tradings.Sales;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Tradings.Sales;

public class GetTotalAmountSalesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/totalamount", HandlerAsync)
            .WithName("TotalAmount: Get the total amount for all sales")
            .WithSummary("Get total amount for all sales")
            .WithDescription("Get total amount for all sales")
            .WithOrder(6)
            .Produces<Response<double?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesHandler handler)
    {
        var request = new GetTotalSalesAmountRequest
        {
            UserId = user.Identity?.Name ?? string.Empty
        };

        var result = await handler.GetTotalAmountSalesRequestAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}