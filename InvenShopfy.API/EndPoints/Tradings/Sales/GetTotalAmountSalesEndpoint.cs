using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Sales;
public class GetTotalAmountSalesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/dashboard/totalamount", HandlerAsync)
            .WithName("TotalAmount: Get the total amount for all sales")
            .WithSummary("Get total amount for all sales")
            .WithDescription("Get total amount for all sales")
            .WithOrder(6)
            .Produces<Response<decimal?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesHandler handler)
    {
     
        var result = await handler.GetTotalAmountSalesRequestAsync();
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}