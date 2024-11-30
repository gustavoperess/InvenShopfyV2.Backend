using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.EndPoints.Identity.Role;

public class GetIdentityRolesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/get-role-custom", Handle);
    
    private static async Task<IResult> Handle(
        ClaimsPrincipal user,
        [FromServices] RoleManager<CustomIdentityRole> roleManager)
    {
        var roles = await roleManager.Roles
            .AsNoTracking()
            .ToListAsync();

        var roleDtos = roles.Select(role => new
        {
            role.Id,
            role.Name,
            role.Description
        });

        return Results.Ok(roleDtos);
    }
}