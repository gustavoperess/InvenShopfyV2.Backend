using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;

namespace InvenShopfy.API.EndPoints.Identity.Role;

public class AssignPermissionsToRoleEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/assign-permissions-to-role", Handle).RequireAuthorization();

    private static async Task<IResult> Handle(
        [FromServices] RoleManager<CustomIdentityRole> roleManager,
        [FromBody] AssignPermissionsRequest request)
    {

        var role = await roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
        {
            return Results.NotFound(new { Message = $"Role '{request.RoleName}' not found." });
        }
        var permissionsJson = JsonConvert.SerializeObject(request.Permissions);
        var result = await roleManager.AddClaimAsync(role, new Claim("Permissions", permissionsJson));
        if (!result.Succeeded)
        {
            return Results.BadRequest(new { Errors = result.Errors });
        }
        return Results.Ok(new { Message = "Permissions assigned successfully." });
    }
}

// Request Model for Assigning Permissions
public class AssignPermissionsRequest
{
    public string RoleName { get; set; } = string.Empty;
    public Permissions Permissions { get; set; } = new Permissions();
}