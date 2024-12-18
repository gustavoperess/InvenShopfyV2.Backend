using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.People;
using InvenShopfy.Core.Models.People.Dto;
using InvenShopfy.Core.Requests.People.Biller;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.People.Biller;

public class GetAllBillersEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Biller: Get All")
            .WithSummary("Get All Billers")
            .WithDescription("Get all Billers")
            .WithOrder(5)
            .Produces<PagedResponse<List<BillerDto>?>>();

    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        IBillerHandler handler,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllBillerRequest
        {
          
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetBillerByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}