using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using InvenShopfy.Core.Requests.UserManagement.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Identity;

public class CreateRoleIdentityEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/roles", Handle).RequireAuthorization();

    private static async Task<IResult> Handle(
        [FromServices] RoleManager<CustomIdentityRole> roleManager,
        [FromBody] CreateRoleRequest request)
    {
        // Check if role already exists
        var roleExists = await roleManager.RoleExistsAsync(request.RoleName);
        if (roleExists)
        {
            return Results.Conflict(new { Message = "Role already exists." });
        }

        // Create new role
        var role = new CustomIdentityRole
        {
            Name = request.RoleName,
            NormalizedName = request.RoleName,
            ConcurrencyStamp = request.ConcurrencyStamp,
            Description = request.Description
        };

        var result = await roleManager.CreateAsync(role);

        if (result.Succeeded)
        {
            return Results.Ok(new { Message = "Role created successfully." });
        }

        return Results.BadRequest(new { Errors = result.Errors });
    }
}
