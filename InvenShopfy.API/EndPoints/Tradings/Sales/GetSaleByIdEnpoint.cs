// using System.Security.Claims;
// using InvenShopfy.API.Common.Api;
// using InvenShopfy.Core.Handlers.Tradings.Sales;
// using InvenShopfy.Core.Requests.Tradings.Sales;
// using InvenShopfy.Core.Responses;
//
// namespace InvenShopfy.API.EndPoints.Tradings.Sales;
//
// public class GetSaleByIdEnpoint : IEndPoint
// {
//     public static void Map(IEndpointRouteBuilder app)
//         => app.MapGet("/single/{id}", HandlerAsync)
//             .WithName("Sales: Get By Id")
//             .WithSummary("Get a sales")
//             .WithDescription("Get a sale")
//             .WithOrder(4)
//             .Produces<Response<Core.Models.Tradings.Sales.Sale?>>();
//
//     private static async Task<IResult> HandlerAsync(
//         ClaimsPrincipal user,
//         ISalesHandler handler,
//         long id)
//     {
//         var request = new GetSalesByIdRequest
//         {
//             UserId = user.Identity?.Name ?? string.Empty,
//             Id = id
//         };
//
//         var result = await handler.GetSaleByIdAsync(request);
//         return result.IsSuccess
//             ? TypedResults.Ok(result)
//             : TypedResults.BadRequest(result);
//     }
// }