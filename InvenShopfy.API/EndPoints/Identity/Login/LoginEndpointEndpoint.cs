using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Data;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;

namespace InvenShopfy.API.EndPoints.Identity.Login;

public class LoginEndpointEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/login-custom", Handle);

    private static async Task<IResult> Handle(
        [FromBody] CustomLoginRequest request,
        [FromServices] AppDbContext dbContext,
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
           
            var rolePermissions = dbContext.RolePermissions 
                .Where(x => x.RoleId == user.RoleId)             
                .Select(rp => new                          
                {
                    rp.EntityType,
                    rp.Action,
                    rp.IsAllowed
                })
                .ToList();
            
            // Create claims for allowed permissions
            var claims = rolePermissions
                .Where(p => p.IsAllowed)  // Only include allowed permissions
                .Select(p => new Claim($"Permission:{p.EntityType}:{p.Action}", "true"))
                .ToList();
            

            // Create a ClaimsIdentity with the permission claims
            var identity = new ClaimsIdentity(claims, "Custom");
            var principal = new ClaimsPrincipal(identity);
            await signInManager.RefreshSignInAsync(user);
            
            return Results.Ok(new { Message = "User logged sucessfully" });
          
        }
        else
        {
            return Results.Unauthorized();
        }
    }
}
