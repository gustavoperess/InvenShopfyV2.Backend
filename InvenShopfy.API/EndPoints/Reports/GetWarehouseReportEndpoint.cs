using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Reports;
using InvenShopfy.Core.Requests.Reports;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Reports;

public class GetWarehouseReportEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/warehouse-report", HandlerAsync)
            .WithName("Reports: Get warehouse report")
            .WithSummary("Get returns the warehouse report with the most import information")
            .WithDescription("Get returns the warehouse report with the most import information")
            .WithOrder(3)
            .Produces<PagedResponse<List<Core.Models.Reports.WarehouseReport>?>>();


    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IReportHandler handler,

        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetReportRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetWarehouseReportAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}