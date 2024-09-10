using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.EndPoints.Identity;

public class GetIdentityRolesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/GetAllRoles", Handle).RequireAuthorization();
    
    private static async Task<IResult> Handle(
        ClaimsPrincipal user,
        [FromServices] RoleManager<IdentityRole<long>> roleManager)
    {
        var roles = await roleManager.Roles.ToListAsync();

        var roleDtos = roles.Select(role => new
        {
            role.Id,
            role.Name,
            role.NormalizedName,
            role.ConcurrencyStamp
        });

        return Results.Ok(roleDtos);
    }
}