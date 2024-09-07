namespace InvenShopfy.API.EndPoints.UserManagement.User;

public class GetUserByIdEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Brands: Get By Id")
            .WithSummary("Get a Brand")
            .WithDescription("Get a Brand")
            .WithOrder(4)
            .Produces<Response<Brand?>>();

    private static async Task<IResult> HandlerAsync(
        // ClaimsPrincipal user,
        IBrandHandler handler,
        long id)
    {
        var request = new GetBrandByIdRequest
        {
            // UserId = user.Identity?.Name ?? string.Empty,
            UserId = "Test@gmail.com",
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}