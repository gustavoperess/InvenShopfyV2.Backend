using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Data;
using InvenShopfy.API.Models;
using InvenShopfy.Core;
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
            // var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:User:View");
            //
            // var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
            //
            // if (!hasPermission)
            // {
            //     return Results.Json(new { data = new List<object>(), message = Configuration.NotAuthorized }, statusCode: 201);
            // }
            
            
            DateTime? onlineThreshold = DateTime.UtcNow.AddMinutes(-10);
            var userRoles = await context.Users  
                .AsNoTracking()
                .Select(ur => new
                {
                    UserId = ur.Id,
                    ur.UserName,
                    ur.Email,
                    ur.DateOfJoin,
                    ur.PhoneNumber,
                    ur.ProfilePicture,
                    RoleName = ur.RoleId == 0 
                        ? "No role assigned yet" 
                        : (context.Roles.Where(role => role.Id == ur.RoleId).Select(role => role.Name).FirstOrDefault() ?? "No role assigned yet"),
                    ur.LastActivityTime,
                    isOnline = ur.LastActivityTime >= onlineThreshold
                })
                .ToListAsync();
            
            return Results.Json(new { data = userRoles, message = "Users retrived sucessfully"}, statusCode: 201);

        }
        catch (Exception e)
        {
            return Results.NotFound($"Error . {e.Message}");
        }
        
    }
}