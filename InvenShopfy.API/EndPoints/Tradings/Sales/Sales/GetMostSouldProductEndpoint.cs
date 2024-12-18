using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Models.Tradings.Sales.Dto;
using InvenShopfy.Core.Requests.Tradings.Sales.Sales;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Tradings.Sales.Sales;

public class GetMostSouldProductEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/dashboard/productmostsold", HandlerAsync)
            .WithName("Sales: Get the product that sold the most in the last month")
            .WithSummary("Get Best Product Seller")
            .WithDescription("Get Best Product Seller")
            .WithOrder(9)
            .Produces<PagedResponse<List<MostSoldProduct>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesHandler handler,
        [FromQuery]DateOnly? startDate=null,
        [FromQuery]DateOnly? endDate=null)
        
    {
        var request = new GetMostSoldProduct
        {
            UserId = user.Identity?.Name ?? string.Empty,
            StartDate = startDate,
            EndDate = endDate
        };
        
        var result = await handler.GetMostSoldProductAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}