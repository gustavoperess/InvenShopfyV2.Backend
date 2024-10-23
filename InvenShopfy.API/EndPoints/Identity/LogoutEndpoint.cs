using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;

namespace InvenShopfy.API.EndPoints.Identity;

public class LogoutEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/logout-custom", HandleAsync).RequireAuthorization();
    
    private static async Task<IResult> HandleAsync(
        SignInManager<CustomUserRequest> signInManage)
    {
        await signInManage.SignOutAsync();
        return Results.Ok();
    }
}