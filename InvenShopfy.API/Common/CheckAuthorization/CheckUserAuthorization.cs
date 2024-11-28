using System.Security.Claims;
using InvenShopfy.API.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InvenShopfy.API.Common.CheckAuthorization;

public class CheckUserAuthorization
{
    private readonly AppDbContext _context;

    public CheckUserAuthorization(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsAuthorizedAsync(ClaimsPrincipal user, string category, string action)
    {
        // Extract role names from user claims
        var roleNames = user.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();

        if (!roleNames.Any())
        {
            return false; // User has no roles
        }

        // Fetch role claims for the user's roles
        var roleClaims = await _context.RoleClaims
            .Where(rc => roleNames.Contains(
                _context.Roles
                    .Where(r => r.Id == rc.RoleId)
                    .Select(r => r.Name)
                    .FirstOrDefault()!))
            .ToListAsync();

        // Check for the required permission in the role claims
        foreach (var roleClaim in roleClaims)
        {
            if (roleClaim.ClaimType == category)
            {
                try
                {
                    var permissions = JsonConvert.DeserializeObject<Dictionary<string, bool>>(roleClaim.ClaimValue);
                    if (permissions != null && permissions.TryGetValue(action, out var isAllowed) && isAllowed)
                    {
                        return true; // User is authorized
                    }
                }
                catch (JsonException ex)
                {
                    // Log the error (optional)
                    Console.WriteLine($"Error deserializing claim value: {ex.Message}");
                    return false;
                }
            }
        }

        return false; // User is not authorized
    }
}