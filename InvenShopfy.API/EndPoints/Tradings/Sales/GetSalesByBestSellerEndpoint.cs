using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Requests.Tradings.Sales;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;


namespace InvenShopfy.API.EndPoints.Tradings.Sales;

public class GetSalesByBestSellerEndpoint : IEndPoint
{

    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/bestseller", HandlerAsync)
            .WithName("Sales: Get the seller that sold the most in the last month")
            .WithSummary("Get Best Seller")
            .WithDescription("Get Best Seller")
            .WithOrder(8)
            .Produces<PagedResponse<List<Core.Models.Tradings.Sales.Sale>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesHandler handler,
        [FromQuery]DateTime? startDate=null,
        [FromQuery]DateTime? endDate=null,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetSalesByBestSeller
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await handler.GetByBestSellerAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }

}