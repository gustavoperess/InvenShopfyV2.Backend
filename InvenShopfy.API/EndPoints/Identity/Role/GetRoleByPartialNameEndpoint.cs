using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvenShopfy.API.Data;

namespace InvenShopfy.API.EndPoints.Identity.Role;

public class GetRoleByPartialNameEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/get-role-by-partial/{name}", Handle);

    private static async Task<IResult> Handle(
        string name,
        ClaimsPrincipal user,
        [FromServices] AppDbContext context,
        [FromServices] RoleManager<CustomIdentityRole> roleManager)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Results.BadRequest("Name parameter cannot be null or empty.");
            }

            var actions = new[] { "Add", "View", "Delete", "Update" };

            var filteredRoles = await context.Roles
                .Where(r => r.Name == name)
                .Select(r => new { r.Id, r.Name })
                .ToListAsync();

            var roleIds = filteredRoles.Select(r => r.Id).ToList();

            var rolePermissions = await context.RolePermissions
                .Where(rp => roleIds.Contains(rp.RoleId))
                .GroupBy(rp => new { rp.RoleId, rp.EntityType })
                .ToListAsync();

            var rolesWithPermissions = rolePermissions
                .GroupBy(g => g.Key.RoleId)
                .Select(roleGroup => new
                {
                    PermissionsByEntity = roleGroup
                        .Select(g => new
                        {
                            g.Key.EntityType,
                            Permissions = actions.Select(action => new
                            {
                                Action = action,
                                IsAllowed = g.Any(x => x.Action == action && x.IsAllowed)
                            }).ToList()
                        })
                        .ToList()
                })
                .ToList();

            return Results.Ok(rolesWithPermissions);
        }
        catch (Exception e)
        {
            return Results.NotFound($"Error . {e.Message}");
        }
    }
}
