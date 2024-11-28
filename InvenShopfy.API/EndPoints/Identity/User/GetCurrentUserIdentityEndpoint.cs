using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.EndPoints.Identity.User;

public class GetCurrentUserIdentityEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("get-current-user", Handle).RequireAuthorization();
    
    private static async Task<IResult> Handle(
        ClaimsPrincipal user,
        [FromServices] AppDbContext context)
    {
        try
        {
            if (user.Identity == null)
            {
                return Results.Unauthorized();
            }
            
            if (!user.Identity.IsAuthenticated || string.IsNullOrEmpty(user.Identity.Name))
            {
                return Results.Unauthorized();
            }
            var currentUser = await context.Users
                .Where(u => u.UserName == user.Identity.Name)
                .Select(u => new
                {
                    u.Id,
                    FullName = u.Name,
                    u.UserName,
                    u.ProfilePicture,
                    u.LastActivityTime,
                    u.PhoneNumber,
                    u.Email,
                    u.Gender,
                    u.DateOfJoin,
                    
                    Roles = context.UserRoles
                        .Where(ur => ur.UserId == u.Id)
                        .Join(context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return currentUser != null ? Results.Ok(currentUser) : Results.NotFound("User not found.");
        }
        catch (Exception e)
        {
            return Results.Problem($"Error retrieving user data: {e.Message}");
        }
    }
}