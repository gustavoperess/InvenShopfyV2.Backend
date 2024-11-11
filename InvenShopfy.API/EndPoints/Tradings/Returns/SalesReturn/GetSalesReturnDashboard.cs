using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Models.Tradings.Returns.SalesReturn.Dto;
using InvenShopfy.Core.Requests.Tradings.Returns.SalesReturn;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Returns.SalesReturn;

public class GetSalesReturnDashboard : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("/dashboard/top-returns", HandleAsync)
        .WithName("SalesReturn: get the top 10 sales returns")
        .WithSummary("SalesReturn et the top 10 sales returns")
        .WithDescription("This endpoint retrive top 10 sales returns")
        .WithOrder(5)
        .Produces<Response<SalesReturnDashboard?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISalesReturnHandler handler)
    {
      
        var request = new GetAllSalesReturnsRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
        };
        
        var result = await handler.GetSaleReturnDashboardAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data}", result)
            : TypedResults.BadRequest(result);

    }
}