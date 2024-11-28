using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Identity.Login;

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
            user = await userManager.FindByEmailAsync(request.UserName);
        }
        if (user == null)
        {
            return Results.NotFound("Invalid Username or Email");
        }
        var result = await signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false, lockoutOnFailure: false);
        
        
        if (result.Succeeded)
        {
            // user.LastLoginTime = DateTime.UtcNow;
            // await userManager.UpdateAsync(user);
            return Results.Ok("Login successful");
        }
        else
        {
            return Results.Unauthorized();
        }
    }
}