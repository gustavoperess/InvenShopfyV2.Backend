using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Requests.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Returns.SalesReturn;

public class GetSalesReturnTotalAmountEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("/total-amount", HandleAsync)
        .WithName("SalesReturn: sales return total amount")
        .WithSummary("SalesReturn get sales return total amount")
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