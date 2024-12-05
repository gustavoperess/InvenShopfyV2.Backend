using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Reports;
using InvenShopfy.Core.Requests.Reports;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Reports;

public class GetCustomerReportEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/customer-report", HandlerAsync)
            .WithName("Reports: Get customer report")
            .WithSummary("Get returns the customer report with the most import information")
            .WithDescription("Get returns the customer report with the most import information")
            .WithOrder(1)
            .Produces<PagedResponse<List<Core.Models.Reports.CustomerReport>?>>();


    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IReportHandler handler,
        [FromQuery]string? dateRange,
        [FromQuery]DateOnly? startDate=null,
        [FromQuery]DateOnly? endDate=null,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Report:View");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        var request = new GetReportRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            DateRange = dateRange, 
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate,
            UserHasPermission = hasPermission,
        };

        var result = await handler.GetCustomerReportAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}