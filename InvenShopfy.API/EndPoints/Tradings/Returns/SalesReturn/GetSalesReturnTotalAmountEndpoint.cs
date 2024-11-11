using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Requests.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Returns.SalesReturn;

public class GetSalesReturnTotalAmountEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("dashboard/total-sold-returned", HandleAsync)
        .WithName("SalesReturn: Gets the total sold amount returned")
        .WithSummary("SalesReturn Gets the total sold amount returned")
        .WithDescription("This endpoint retrive sales return total amount")
        .WithOrder(5)
        .Produces<Response<decimal?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISalesReturnHandler handler)
    {
      
        var request = new GetAllSalesReturnsRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
        };
        
        var result = await handler.GetTotalSalesReturnAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data}", result)
            : TypedResults.BadRequest(result);

    }
}