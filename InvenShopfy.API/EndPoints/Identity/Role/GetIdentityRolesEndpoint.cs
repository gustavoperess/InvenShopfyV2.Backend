using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using InvenShopfy.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.EndPoints.Identity.Role;

public class GetIdentityRolesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/get-role-custom", Handle);
    
    private static async Task<IResult> Handle(
        ClaimsPrincipal user,
        [FromServices] RoleManager<CustomIdentityRole> roleManager)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Roles:View");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";

        if (!hasPermission)
        {
            return Results.Json(new { data = new List<object>(), message = Configuration.NotAuthorized }, statusCode: 201);
        }

        var roles = await roleManager.Roles
            .AsNoTracking()
            .ToListAsync();

        var roleDtos = roles.Select(role => new
        {
            role.Id,
            RoleName = role.Name,
            role.Description
        });
        return Results.Json(new { data = roleDtos, message = "Roles retrived sucessfully"}, statusCode: 201);
        
    }
}