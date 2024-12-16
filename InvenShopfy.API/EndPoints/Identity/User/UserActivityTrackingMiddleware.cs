using System.Security.Claims;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;

namespace InvenShopfy.API.EndPoints.Identity.User;

public class UserActivityTrackingMiddleware
{
    private readonly RequestDelegate _next;

    public UserActivityTrackingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, UserManager<CustomUserRequest> userManager)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    if (user.LastActivityTime == null || user.LastActivityTime < DateTime.UtcNow.AddMinutes(-5))
                    {
                        user.LastActivityTime = DateTime.UtcNow;
                        await userManager.UpdateAsync(user);
                    }
                }
            }
        }

        await _next(context);
    }
}