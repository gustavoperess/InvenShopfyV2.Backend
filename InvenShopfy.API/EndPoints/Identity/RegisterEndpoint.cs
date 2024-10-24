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

            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.UserName))
            {
                return Results.BadRequest("Email and Username are required.");
            }

            var userByEmail = await userManager.FindByEmailAsync(request.Email);
            var userByUsername = await userManager.FindByNameAsync(request.UserName);

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
            };
            if (string.IsNullOrWhiteSpace(request.PasswordHash))
            {
                return Results.BadRequest("Password is required.");
            }

            // Create the user in the database
            var resultCreate = await userManager.CreateAsync(user, request.PasswordHash);
            if (!resultCreate.Succeeded)
            {
                var createErrors = string.Join(", ", resultCreate.Errors.Select(e => e.Description));
                return Results.BadRequest($"Create Errors: {createErrors}");
            }
            
            // Find the staff role by name
            var normalizedRoleName = roleManager.NormalizeKey(request.RoleName);
            var role = await roleManager.FindByNameAsync(normalizedRoleName);
            if (role != null)
            {
                // Assign the role to the user
                var resultAssignRole = await userManager.AddToRoleAsync(user, normalizedRoleName);
                if (!resultAssignRole.Succeeded)
                {
                    var roleErrors = string.Join(", ", resultAssignRole.Errors.Select(e => e.Description));
                    return Results.BadRequest($"Role Assignment Errors: {roleErrors}");
                }
            }
            else
            {
                return Results.BadRequest($"Role {role?.Name} does not exist.");
            }
            
            return Results.Ok("User registered successfully");
        }
    }
}