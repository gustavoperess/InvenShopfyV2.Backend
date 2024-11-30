using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.EndPoints.Identity.User;

public class GetIdentityUsersEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/get-user-custom", Handle).RequireAuthorization();
    
    private static async Task<IResult> Handle(
        ClaimsPrincipal user,
        [FromServices] AppDbContext context)
    {
        try
        {
            DateTime? onlineThreshold = DateTime.UtcNow.AddMinutes(-10);
            var userRoles = await context.Set<IdentityUserRole<long>>()
                .AsNoTracking()
                .Join(context.Users,
                    userRole => userRole.UserId,
                    userinfo => userinfo.Id,
                    (userRole, userinfo) => new { userRole.RoleId, User = userinfo })
                .Join(context.Roles,
                    ur => ur.RoleId,
                    role => role.Id,
                    (ur, role) => new
                    {
                        UserId = ur.User.Id,
                        ur.User.DateOfJoin,
                        ur.User.PhoneNumber,
                        ur.User.Email,
                        ur.User.ProfilePicture,
                        UserName = ur.User.Name,
                        RoleName = role.Name,
                        ur.User.LastActivityTime,
                        isOnline = ur.User.LastActivityTime >= onlineThreshold
                    })
                .ToListAsync();
            
            return Results.Ok(userRoles);
        }
        catch (Exception e)
        {
            return Results.NotFound($"Error . {e.Message}");
        }
        
    }
}