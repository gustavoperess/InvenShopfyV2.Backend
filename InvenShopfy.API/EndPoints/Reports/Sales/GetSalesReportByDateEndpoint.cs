using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Reports;
using InvenShopfy.Core.Handlers.Reports.Sales;
using InvenShopfy.Core.Requests.Reports;
using InvenShopfy.Core.Requests.Reports.Sales;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Reports.Sales;

public class GetSalesReportByDateEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Reports: Get by date")
            .WithSummary("Get a sale report by  date")
            .WithDescription("Get a sale report by a date")
            .WithOrder(1)
            .Produces<PagedResponse<List<Core.Models.Reports.SaleReport>?>>();


    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISalesReportHandler handler,
        [FromQuery]DateOnly? startDate=null,
        [FromQuery]DateOnly? endDate=null,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetSalesReportByDateRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await handler.GetByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}