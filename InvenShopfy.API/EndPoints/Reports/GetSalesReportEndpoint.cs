using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Reports;
using InvenShopfy.Core.Requests.Reports;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Reports;

public class GetSalesReportEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/sales-report", HandlerAsync)
            .WithName("Reports: Get Sales report")
            .WithSummary("Get returns the sale report with the most import information")
            .WithDescription("Get returns the sale report with the most import information")
            .WithOrder(1)
            .Produces<PagedResponse<List<Core.Models.Reports.SaleReport>?>>();


    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IReportHandler handler,
        [FromQuery]string? reportShortCut,
        [FromQuery]DateOnly? startDate=null,
        [FromQuery]DateOnly? endDate=null,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetSalesReportRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            ReportShortCut = reportShortCut, 
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await handler.GetSalesReportAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}