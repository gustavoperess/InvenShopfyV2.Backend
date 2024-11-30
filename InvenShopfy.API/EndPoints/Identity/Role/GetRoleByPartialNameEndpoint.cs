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
            
            var roles = await context.Roles
                .AsNoTracking()
                .Where(x => EF.Functions.ILike(x.Name ?? "", $"%{name}%")) 
                .Select(g => new
                {
                    RoleName = g.Name,
                    g.Id
                }).ToListAsync();

            return Results.Ok(roles);
        }
        catch (Exception e)
        {
            return Results.NotFound($"Error . {e.Message}");
        }
    }
}




