using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using InvenShopfy.API.Data;
using Newtonsoft.Json;

namespace InvenShopfy.API.EndPoints.Identity.Role;

public class AssignPermissionsToRoleEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/assign-permissions-to-role", Handle).RequireAuthorization();

    private static async Task<IResult> Handle(
        [FromServices] RoleManager<CustomIdentityRole> roleManager,
        [FromServices] AppDbContext dbContext,
        [FromBody] RolePermissionRequest request)
    {
        // Fetch the role
        var role = await roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
        {
            return Results.NotFound(new { Message = $"Role '{request.RoleName}' not found." });
        }

        // Remove existing permissions for this role
        var existingPermissions = dbContext.RolePermissions.Where(rp => rp.RoleId == role.Id);
        dbContext.RolePermissions.RemoveRange(existingPermissions);

        // Add new permissions
        var newPermissions = request.Permissions.Select(p => new RolePermission
        {
            RoleId = role.Id,  
            EntityType = p.EntityType,
            Action = p.Action,
            IsAllowed = p.IsAllowed
        });

        await dbContext.RolePermissions.AddRangeAsync(newPermissions);
        await dbContext.SaveChangesAsync();

        return Results.Ok(new { Message = "Permissions assigned successfully." });
    }
}


