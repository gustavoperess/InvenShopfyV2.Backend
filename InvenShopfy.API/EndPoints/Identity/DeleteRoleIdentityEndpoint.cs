using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using InvenShopfy.Core.Requests.UserManagement.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Identity;

public class DeleteRoleIdentityEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/delete-role-custom/{id}",
            HandlerAsync).RequireAuthorization();
    private static async Task<IResult> HandlerAsync(
        [FromServices] RoleManager<CustomIdentityRole> roleManager,
        long id
        )
    {
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