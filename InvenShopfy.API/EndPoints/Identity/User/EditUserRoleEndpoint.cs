using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using InvenShopfy.Core.Requests.UserManagement.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Identity.User;

public class EditUserRoleEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/edit-user-role/{userId}", Handle);

    private static async Task<IResult> Handle(
        [FromBody] UpdateUserRoleRequest request,
        [FromServices] RoleManager<CustomIdentityRole> roleManager,
        [FromServices] UserManager<CustomUserRequest> userManager,
        string userId)
    {
 
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return Results.BadRequest($"User with ID '{userId}' not found.");
        
        var role = await roleManager.FindByIdAsync(request.UserRoleId.ToString());
        if (role == null)
            return Results.BadRequest($"Role with ID '{request.UserRoleId}' not found.");

        if (string.IsNullOrEmpty(role.Name))
            return Results.BadRequest("Role name is invalid.");

        // Remove User From Existing Roles
        var currentRoles = await userManager.GetRolesAsync(user);
        if (currentRoles.Any())
        {
            var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                var errors = string.Join(", ", removeResult.Errors.Select(e => e.Description));
                return Results.BadRequest($"Error removing existing roles: {errors}");
            }
        }

        // Assign New Role
        var addToRoleResult = await userManager.AddToRoleAsync(user, role.Name);
        if (!addToRoleResult.Succeeded)
        {
            var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
            return Results.BadRequest($"Error adding role: {errors}");
        }

        // Update User Role ID and Persist
        user.RoleId = request.UserRoleId;
        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
            return Results.BadRequest($"Error updating user: {errors}");
        }

        return Results.Ok("User role updated successfully.");
    }
}
