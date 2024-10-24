using InvenShopfy.API.Common.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InvenShopfy.API.Models;

namespace InvenShopfy.API.EndPoints.Identity;

public class LoginEndpointEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/login-custom", Handle);

    private static async Task<IResult> Handle(
        [FromBody] CustomLoginRequest request,
        [FromServices] UserManager<CustomUserRequest> userManager,
        [FromServices] SignInManager<CustomUserRequest> signInManager)
    {
        
        var user = await userManager.FindByNameAsync(request.UserName);
        if (user == null)
        {
            user = await userManager.FindByEmailAsync(request.Email);
        }
        if (user == null)
        {
            return Results.NotFound("User not found");
        }

        var result = await signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return Results.Ok("Login successful");
        }
        else
        {
            return Results.Unauthorized();
        }
    }
}