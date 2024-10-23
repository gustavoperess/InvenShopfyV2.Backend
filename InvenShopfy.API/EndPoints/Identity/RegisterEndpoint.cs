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
            
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.UserName))
        {
            Console.WriteLine("THIS HAPPENED?");
            return Results.BadRequest("Email and Username are required.");
        }

        var userByEmail = await userManager.FindByEmailAsync(request.Email);
        var userByUsername = await userManager.FindByNameAsync(request.UserName);
        
        // Check if the user already exists by username or email
        if (userByEmail != null || userByUsername != null)
        {
            return Results.BadRequest("User already exists with this email or username.");
        }

        // Create the user
        var user = new CustomUserRequest
        {
            Name = request.Name,
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            SecurityStamp = Guid.NewGuid().ToString(), 
            RoleName = request.RoleName
        };

        if (string.IsNullOrWhiteSpace(request.PasswordHash))
        {
            Console.WriteLine("ISSUE WITH THE PASSWORD");
            return Results.BadRequest("Password is required.");
        }

        // Create the user in the database
        var resultCreate = await userManager.CreateAsync(user, request.PasswordHash);
       
        if (!resultCreate.Succeeded)
        {
            var createErrors = string.Join(", ", resultCreate.Errors.Select(e => e.Description));
            return Results.BadRequest($"Create Errors: {createErrors}");
        }
        
        var resultAddRole = await userManager.AddToRoleAsync(user, request.RoleName);
        
        if (!resultAddRole.Succeeded)
        {
            var roleErrors = string.Join(", ", resultAddRole.Errors.Select(e => e.Description));
            return Results.BadRequest($"Role Errors: {roleErrors}");
        }

            return Results.Ok("User registered successfully");
        }
    }
}