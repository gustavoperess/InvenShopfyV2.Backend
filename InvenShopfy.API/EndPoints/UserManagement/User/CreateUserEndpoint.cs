namespace InvenShopfy.API.EndPoints.UserManagement.User;

public class CreateUserEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
        .WithName("Brands: Create Brand")
        .WithSummary("Create a new brand")
        .WithDescription("Create a new brand")
        .WithOrder(1)
        .Produces<Response<Brand?>>();

    private static async Task<IResult> HandleAsync(
        IBrandHandler handler,
        CreateBrandRequest request)
    {
        request.UserId = "Test@gmail.com";
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result.Data);
        
    }
}