using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Reports;
using InvenShopfy.Core.Requests.Reports;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Reports.Sales;

public class GetSalesReportByDateEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{startdate}", HandlerAsync)
            .WithName("Reports: Get by date")
            .WithSummary("Get sales by a date")
            .WithDescription("Get sales by a date")
            .WithOrder(4)
            .Produces<PagedResponse<List<Core.Models.Reports.SaleReport>?>>();


    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesReportHandler handler,
        [FromQuery]DateTime? startDate=null,
        [FromQuery]DateTime? endDate=null,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetSalesReportRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}