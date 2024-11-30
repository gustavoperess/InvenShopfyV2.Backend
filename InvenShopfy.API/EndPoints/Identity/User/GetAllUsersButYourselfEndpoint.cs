using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.EndPoints.Identity.User;

public class GetAllUsersButYourselfEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/users-but-yourself", Handle).RequireAuthorization();

    private static async Task<IResult> Handle(
        ClaimsPrincipal user,
        [FromServices] AppDbContext context)
    {
        try
        {
            if (user.Identity == null)
            {
                return Results.Unauthorized();
            }

            if (!user.Identity.IsAuthenticated || string.IsNullOrEmpty(user.Identity.Name))
            {
                return Results.Unauthorized();
            }

            var query = context.Users
                .AsNoTracking().
                Where(x => x.UserName != user.Identity.Name);
            
            var users = await query.Select(x => new
            {
                userId = x.Id,
                userName = x.Name.Substring(0, x.Name.IndexOf(" ")) + 
                       x.Name.Substring(x.Name.LastIndexOf(" "))
                
            }).ToListAsync();

            return Results.Ok(users);
        }
        catch (Exception e)
        {
            return Results.NotFound($"Error . {e.Message}");
        }
    }
}