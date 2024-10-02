using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.Core.Handlers.Tradings.Purchase;
using InvenShopfy.Core.Handlers.Tradings.Sales;
using InvenShopfy.Core.Requests.Tradings.Sales;
using InvenShopfy.Core.Responses;

namespace InvenShopfy.API.EndPoints.Tradings.Sales;

public class CreateSaleEndpoint  : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
            .WithName("Sales: Add a sale")
            .WithSummary("Create: a sale")
            .WithDescription("Create: a sale")
            .WithOrder(1)
            .Produces<Response<Core.Models.Tradings.Sales.Sale?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ISalesHandler handler,
            CreateSalesRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateAsync(request);
            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);

        }
}