using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Reports;
using InvenShopfy.Core.Requests.Reports;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Reports;

public class GetExpenseReportEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/expense-report", HandlerAsync)
            .WithName("Reports: Get expense report")
            .WithSummary("Get returns the expense report with the most import information")
            .WithDescription("Get returns the expense report with the most import information")
            .WithOrder(2)
            .Produces<PagedResponse<List<Core.Models.Reports.ExpenseReport>?>>();


    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IReportHandler handler,
        [FromQuery]string? dateRange,
        [FromQuery]DateOnly? startDate=null,
        [FromQuery]DateOnly? endDate=null,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetReportRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            DateRange = dateRange, 
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await handler.GetExpenseReportAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}