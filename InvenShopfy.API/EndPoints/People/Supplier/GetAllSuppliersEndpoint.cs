using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Requests.People.Customer;
using InvenShopfy.Core.Requests.People.Supplier;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.People.Supplier;

public class GetAllSuppliersEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Suppliers: Get All")
            .WithSummary("Get All Suppliers")
            .WithDescription("Get all Suppliers")
            .WithOrder(5)
            .Produces<PagedResponse<List<Core.Models.People.Supplier>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        ISupplierHandler handler,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllSuppliersRequest
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