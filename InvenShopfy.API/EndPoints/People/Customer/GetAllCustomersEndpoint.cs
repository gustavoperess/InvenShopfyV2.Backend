using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Requests.People.Biller;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.People.Customer;

public class GetAllCustomersEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Customers: Get All")
            .WithSummary("Get All Customers")
            .WithDescription("Get all Customers")
            .WithOrder(5)
            .Produces<PagedResponse<List<Core.Models.People.Customer>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ICustomerHandler handler,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Customer:View");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        var request = new GetAllCustomersRequest
        {
            UserHasPermission = hasPermission,
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetCustomerByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}