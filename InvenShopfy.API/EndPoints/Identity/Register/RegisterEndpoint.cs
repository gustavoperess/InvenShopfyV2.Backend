using System.Globalization;
using System.Security.Claims;
using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Common.CloudinaryServiceNamespace;
using InvenShopfy.API.Models;
using InvenShopfy.Core.Requests.UserManagement.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Identity.Register
// NEED TO CHECK WHY IT IS NOT ADDING TO THE ROLES, SOMETHING TO DO WITH THE NORMALIZED NAME
{
        public class RegisterEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/register-custom", Handle);

        private static async Task<IResult> Handle(
            [FromBody] CreateUserRequest request,
            CloudinaryService cloudinaryService,
            ClaimsPrincipal principal,
            [FromServices] RoleManager<CustomIdentityRole> roleManager,
            [FromServices] UserManager<CustomUserRequest> userManager)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.UserName))
            {
                return Results.BadRequest("Email and Username are required.");
            }

            var userByEmail = await userManager.FindByEmailAsync(request.Email);
            var userByUsername = await userManager.FindByNameAsync(request.UserName);

            if (userByEmail != null)
            {
                return Results.BadRequest("Email address already in use, please choose a different one");
            }
            
            if (userByUsername != null)
            {
                return Results.BadRequest("Username already in use, please choose a different one");
            }
         
            // Create the user
            var user = new CustomUserRequest
            {
                Name = textInfo.ToTitleCase(request.Name),
                UserName = textInfo.ToTitleCase(request.UserName),
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                SecurityStamp = Guid.NewGuid().ToString(), 
                LastActivityTime = DateTime.UtcNow,
                RoleId = request.RoleId
            };
            
            if (request.ProfilePicture == null)
            {
                user.ProfilePicture =
                    "https://res.cloudinary.com/dououppib/image/upload/v1709830638/PLANTS/placeholder_ry6d8v.webp";
            }
            else
            {
               
                var uploadResult = await cloudinaryService.UploadImageAsync(request.ProfilePicture, "invenShopfy/ProfilePictures");
                user.ProfilePicture = uploadResult.SecureUrl.ToString(); 
            
            }
            
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
            
            return Results.Ok("User registered successfully");
        }
    }
}