using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core;
using InvenShopfy.Core.Handlers.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Requests.Tradings.Returns.PurchaseReturn;
using InvenShopfy.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Tradings.Returns.PurchaseReturn;

public class GetAllPurchaseReturnsEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync)
        .WithName("PurchaseReturn: get all PurchaseReturn")
        .WithSummary("PurchaseReturn get all PurchaseReturn")
        .WithDescription("This endpoint retrive all PurchaseReturn")
        .WithOrder(3)
        .Produces<Response<Core.Models.Tradings.Returns.PurchaseReturn.PurchaseReturn?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IPurchaseReturnHandler handler,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
      
        var request = new GetAllPurchaseReturnsRequests
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        
        var result = await handler.GetAllPurchaseReturnAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data}", result)
            : TypedResults.BadRequest(result);

    }
}