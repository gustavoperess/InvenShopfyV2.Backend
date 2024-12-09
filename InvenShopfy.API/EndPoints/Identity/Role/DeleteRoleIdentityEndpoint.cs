using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InvenShopfy.Core;

namespace InvenShopfy.API.EndPoints.Identity.Role;

public class DeleteRoleIdentityEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/delete-role-custom/{id}",
            HandlerAsync).RequireAuthorization();
    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        [FromServices] RoleManager<CustomIdentityRole> roleManager,
        long id
        )
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:Roles:Delete");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        if (!hasPermission)
        {
            return Results.Json(new { data = new List<object>(), message = Configuration.NotAuthorized }, statusCode: 201);
        }
        
        var role = await roleManager.FindByIdAsync(id.ToString());
        if (role == null)
        {
            return Results.BadRequest(new { Errors = "Error in finding the role" });
        } 
        
        var result = await roleManager.DeleteAsync(role);

        if (result.Succeeded)
        {
            return Results.Ok(new { Message = "Role deleted successfully." });
        }

        return Results.BadRequest(new { Errors = result.Errors });
    }
}