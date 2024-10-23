using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Data;
using InvenShopfy.API.Models;
using InvenShopfy.Core.Requests.UserManagement.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Identity
// NEED TO CHECK WHY IT IS NOT ADDING TO THE ROLES, SOMETHING TO DO WITH THE NORMALIZED NAME
{
        public class RegisterEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/register-custom", Handle);

        private static async Task<IResult> Handle(
            [FromBody] CreateUserRequest request,
            [FromServices] RoleManager<CustomIdentityRole> roleManager,
            [FromServices] UserManager<CustomUserRequest> userManager)
        {
            Console.WriteLine("Starting user registration...");

            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.UserName))
            {
                Console.WriteLine("Email or Username is missing.");
                return Results.BadRequest("Email and Username are required.");
            }

            var userByEmail = await userManager.FindByEmailAsync(request.Email);
            var userByUsername = await userManager.FindByNameAsync(request.UserName);

            if (userByEmail != null || userByUsername != null)
            {
                Console.WriteLine($"User already exists with email: {request.Email} or username: {request.UserName}");
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
            };

            if (string.IsNullOrWhiteSpace(request.PasswordHash))
            {
                Console.WriteLine("Password is missing.");
                return Results.BadRequest("Password is required.");
            }

            // Create the user in the database
            Console.WriteLine("Creating user...");
            var resultCreate = await userManager.CreateAsync(user, request.PasswordHash);
            if (!resultCreate.Succeeded)
            {
                var createErrors = string.Join(", ", resultCreate.Errors.Select(e => e.Description));
                Console.WriteLine($"Error creating user: {createErrors}");
                return Results.BadRequest($"Create Errors: {createErrors}");
            }

            // Find the staff role by name
            var staffRole = await roleManager.FindByNameAsync("staff");
            if (staffRole != null)
            {
                Console.WriteLine($"Role 'staff' exists. Role ID: {staffRole.Id}");

                // Assign the role to the user
                var resultAssignRole = await userManager.AddToRoleAsync(user, staffRole.Name);
                if (!resultAssignRole.Succeeded)
                {
                    var roleErrors = string.Join(", ", resultAssignRole.Errors.Select(e => e.Description));
                    Console.WriteLine($"Error assigning role 'staff': {roleErrors}");
                    return Results.BadRequest($"Role Assignment Errors: {roleErrors}");
                }
            }
            else
            {
                Console.WriteLine("Role 'staff' does not exist.");
                return Results.BadRequest("Role 'staff' does not exist.");
            }

            Console.WriteLine("User registration successful.");
            return Results.Ok("User registered successfully");
        }
    }
}