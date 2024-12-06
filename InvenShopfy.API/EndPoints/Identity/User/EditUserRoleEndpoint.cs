using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Data;
using InvenShopfy.API.Models;
using InvenShopfy.Core;
using InvenShopfy.Core.Requests.UserManagement.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.EndPoints.Identity.User;

public class EditUserRoleEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/edit-user-role/{userId}", Handle);

    private static async Task<IResult> Handle(
        [FromBody] UpdateUserRoleRequest request,
        [FromServices] RoleManager<CustomIdentityRole> roleManager,
        [FromServices] AppDbContext context,
        ClaimsPrincipal cuser,
        [FromServices] UserManager<CustomUserRequest> userManager,
        string userId)
    {
        var permissionClaim = cuser.Claims.FirstOrDefault(c => c.Type == "Permission:Roles:Update");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        if (!hasPermission)
        {
            return Results.Json(new { data = string.Empty, message = Configuration.NotAuthorized }, statusCode: 409);
        }
      


        // Step 1: Validate User
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return Results.BadRequest($"User with ID '{userId}' not found.");

        // Step 2: Validate Role
        var role = await roleManager.FindByIdAsync(request.UserRoleId.ToString());
        if (role == null || string.IsNullOrEmpty(role.Name))
            return Results.BadRequest($"Invalid role with ID '{request.UserRoleId}'.");

        // Step 3: Remove Existing Permission Claims
        var existingClaims = await userManager.GetClaimsAsync(user);
        var permissionClaims = existingClaims.Where(c => c.Type.StartsWith("Permission:")).ToList();
        foreach (var claim in permissionClaims)
        {
            var result = await userManager.RemoveClaimAsync(user, claim);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Results.BadRequest($"Error removing claims: {errors}");
            }
        }

        // Step 4: Add New Permission Claims
        var permissions = await context.RolePermissions
            .AsNoTracking()
            .Where(p => p.RoleId == request.UserRoleId) 
            .ToListAsync();
        
        foreach (var permission in permissions)
        {
            var claim = new Claim($"Permission:{permission.EntityType}:{permission.Action}", permission.IsAllowed.ToString());
            var result = await userManager.AddClaimAsync(user, claim);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Results.BadRequest($"Error adding claims: {errors}");
            }
        }

        // Step 5: Remove Existing Roles
        var currentRoles = await userManager.GetRolesAsync(user);
        if (currentRoles.Any())
        {
            var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                var errors = string.Join(", ", removeResult.Errors.Select(e => e.Description));
                return Results.BadRequest($"Error removing roles: {errors}");
            }
        }

        // Step 6: Assign New Role
        var addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
        if (!addRoleResult.Succeeded)
        {
            var errors = string.Join(", ", addRoleResult.Errors.Select(e => e.Description));
            return Results.BadRequest($"Error adding role: {errors}");
        }

        // Step 7: Update User Role 
        user.RoleId = request.UserRoleId;
        var updateUserResult = await userManager.UpdateAsync(user);
        if (!updateUserResult.Succeeded)
        {
            var errors = string.Join(", ", updateUserResult.Errors.Select(e => e.Description));
            return Results.BadRequest($"Error updating user: {errors}");
        }

        // Final Response
        return Results.Ok("User role and claims updated successfully.");
    }
}
