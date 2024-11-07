using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Data;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.EndPoints.Identity;

public class GetManagerOrAdminEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/get-manager-admin-custom", Handle).RequireAuthorization();
    
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
                        RoleName = role.Name
                    })
                .Where(x => x.RoleName == "Admin" || x.RoleName == "Manager")
                .ToListAsync();
            
            return Results.Ok(userRoles);
        }
        catch (Exception e)
        {
            return Results.NotFound($"Error . {e.Message}");
        }
        
    }
}