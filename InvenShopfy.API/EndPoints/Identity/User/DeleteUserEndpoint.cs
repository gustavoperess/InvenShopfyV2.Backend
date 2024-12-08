using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using InvenShopfy.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Identity.User;

public class DeleteUserEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/delete-user/{id}",
            HandlerAsync).RequireAuthorization();
    private static async Task<IResult> HandlerAsync(
        ClaimsPrincipal user,
        [FromServices] UserManager<CustomUserRequest> userManager,
        long id)
    {
        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permission:User:Delete");
        var hasPermission = permissionClaim != null && permissionClaim.Value == "True";
        
        if (!hasPermission)
        {
            return Results.Json(new { data = string.Empty, message = Configuration.NotAuthorized }, statusCode: 409);
        }
        
        var userToDelete = await userManager.FindByIdAsync(id.ToString());
        if (userToDelete == null)
        {
            return Results.BadRequest(new { Errors = "Error in finding the role" });
        } 
        
        var result = await userManager.DeleteAsync(userToDelete);
        
        if (result.Succeeded)
        {
            return Results.Ok(new { Message = "User deleted successfully." });
        }

        return Results.BadRequest(new { Errors = result.Errors });
    }
}