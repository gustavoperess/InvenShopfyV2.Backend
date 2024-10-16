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
            // Console.WriteLine("NOTHING IS HAPPENING WHY THIS IS NOT CALLIMG????");
            // Console.WriteLine(request.Name);
            // Console.WriteLine(request.Email);
            // Console.WriteLine(request.PhoneNumber);
            // Console.WriteLine(request.UserName);
            // Console.WriteLine(request.PasswordHash);
            Console.WriteLine("CHECK ROLES NAME HERE 1");
            Console.WriteLine(request.RoleName);
            
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
            };

            if (string.IsNullOrWhiteSpace(request.PasswordHash))
            {
                Console.WriteLine("ISSUE WITH THE PASSOWRD");
                return Results.BadRequest("Password is required.");
            }
            var resultCreate = await userManager.CreateAsync(user, request.PasswordHash);
            var resultAddRole = await userManager.AddToRoleAsync(user, request.RoleName);
            if (resultCreate.Succeeded && resultAddRole.Succeeded)
            {
                return Results.Ok("User registered successfully");
            }
            var createErrors = string.Join(", ", resultCreate.Errors.Select(e => e.Description));
            var roleErrors = string.Join(", ", resultAddRole.Errors.Select(e => e.Description));
            return Results.BadRequest($"Create Errors: {createErrors}, Role Errors: {roleErrors}");
        }
    }
}