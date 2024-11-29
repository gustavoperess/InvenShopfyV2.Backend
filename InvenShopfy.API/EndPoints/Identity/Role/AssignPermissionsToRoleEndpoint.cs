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
        ClaimsPrincipal user,
        [FromServices] UserManager<CustomUserRequest> userManager,
        [FromBody] RolePermissionRequest request)
    {
        // Fetch the role
        var role = await roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
        {
            return Results.NotFound(new { Message = $"Role '{request.RoleName}' not found." });
        }
        request.UserId = user.Identity?.Name ?? string.Empty;
    
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
        
        var roleUsers = dbContext.UserRoles.Where(ur => ur.RoleId == role.Id).Select(ur => ur.UserId).ToList();
        
        foreach (var userId in roleUsers)
        {
            var currentUser = await userManager.FindByIdAsync(userId.ToString());
        
            if (currentUser != null)
            {
                var existingClaims = await userManager.GetClaimsAsync(currentUser);
                var permissionClaims = existingClaims.Where(c => c.Type.StartsWith("Permission:")).ToList();
                foreach (var claim in permissionClaims)
                {
                    await userManager.RemoveClaimAsync(currentUser, claim);
                }
                // Add new permission claims based on the new RolePermissions
                foreach (var permission in newPermissions)
                {
                    var claim = new Claim($"Permission:{permission.EntityType}:{permission.Action}", permission.IsAllowed.ToString());
                    var claimResult = await userManager.AddClaimAsync(currentUser, claim);
                    if (!claimResult.Succeeded)
                    {
                        return Results.BadRequest($"Failed to add claim for user: {currentUser.UserName}");
                    }
                }
            }
        }
        
        return Results.Ok(new { Message = "Permissions assigned successfully." });
    }
}


