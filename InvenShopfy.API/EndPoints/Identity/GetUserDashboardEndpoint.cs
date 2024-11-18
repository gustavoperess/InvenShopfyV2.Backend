using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace InvenShopfy.API.EndPoints.Identity;

public class GetUserDashboardEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("dashboard/get-user-dashboard", Handle).RequireAuthorization();
    
    private static async Task<IResult> Handle(
        ClaimsPrincipal user,
        [FromServices] AppDbContext context)
    {
        try
        {
            var userRoles = await context.Set<IdentityUserRole<long>>()
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
                        UserName = ur.User.Name,
                        ur.User.ProfilePicture,
                        RoleName = role.Name,
                        LastLogin = ur.User.LastLoginTime
                    }).OrderByDescending(x => x.LastLogin).Take(5)
                .ToListAsync();
            
            return Results.Ok(userRoles);
        }
        catch (Exception e)
        {
            return Results.NotFound($"Error . {e.Message}");
        }
        
    }
}