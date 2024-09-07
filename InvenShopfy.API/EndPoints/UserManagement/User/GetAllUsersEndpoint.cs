using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.UserManagement;
using InvenShopfy.Core.Requests.Products.Brand;
using InvenShopfy.Core.Requests.UserManagement.User;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.UserManagement.User;

public class GetAllUsersEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Users: Get All")
            .WithSummary("Get All Users")
            .WithDescription("Get all Users")
            .WithOrder(5)
            .Produces<PagedResponse<List<Core.Models.UserManagement.User>?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IUserManagementUserHandler handler,
        [FromQuery]DateTime? startDate=null,
        [FromQuery]DateTime? endDate=null,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllUsersRequest
        {
            // UserId = user.Identity?.Name ?? string.Empty,
            UserId = "Test@gmail.com",
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetByPeriodAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}