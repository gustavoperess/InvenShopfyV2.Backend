using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Identity
{
    public class RegisterEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/register-custom", Handle);

        private static async Task<IResult> Handle(
            [FromBody] CustomUserRequest request,
            [FromServices] UserManager<CustomUserRequest> userManager)
        {
            // Check if the user already exists by username or email
            var userByEmail = await userManager.FindByEmailAsync(request.Email);
            var userByUsername = await userManager.FindByNameAsync(request.UserName);

            if (userByEmail != null || userByUsername != null)
            {
                return Results.BadRequest("User already exists with this email or username.");
            }

            // Create the user
            var user = new CustomUserRequest
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Name = request.Name,
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return Results.Ok("User registered successfully");
            }

            return Results.BadRequest(result.Errors);
        }
    }
}