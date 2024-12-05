using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Reports;
using InvenShopfy.Core.Requests.Reports;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Reports; 

public class GetProductReportEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/product-report", HandlerAsync)
            .WithName("Reports: Get product report")
            .WithSummary("Get returns the product report with the most import information")
            .WithDescription("Get returns the product report with the most import information")
            .WithOrder(3)
            .Produces<PagedResponse<List<Core.Models.Reports.ProductReport>?>>();


    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IReportHandler handler,
      
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Reports:View");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        var request = new GetReportRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
            UserHasPermission = hasPermission
  
        };

        var result = await handler.GetProductReportAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}