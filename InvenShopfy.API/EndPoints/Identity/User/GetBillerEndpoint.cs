using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvenShopfy.API.EndPoints.Identity.User;

public class GetBillerEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/get-billers", Handle).RequireAuthorization();
    private static async Task<IResult> Handle(
        ClaimsPrincipal user,
        [FromServices] AppDbContext context)
    {
        try
        {
            var userWithPermissions = await context.Users
                .AsNoTracking()
                .Join(context.UserClaims,
                    userInfo => userInfo.Id,
                    claim => claim.UserId,
                    (userInfo, claim) => new { User = userInfo, Claim = claim })
                .Where(uc => uc.Claim.ClaimType == "Permission:Sales:Add" && uc.Claim.ClaimValue == "True")
                .Select(uc => new
                {
                    UserId = uc.User.Id,
                    uc.User.UserName,

                }).ToListAsync();
            
            return Results.Ok(userWithPermissions);
        }
        catch (Exception e)
        {
            return Results.NotFound($"Error . {e.Message}");
        }

    }
}