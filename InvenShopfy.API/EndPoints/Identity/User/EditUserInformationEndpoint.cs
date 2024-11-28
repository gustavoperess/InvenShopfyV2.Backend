using InvenShopfy.API.Common.Api;
using InvenShopfy.API.Common.CloudinaryServiceNamespace;
using InvenShopfy.API.Models;
using InvenShopfy.Core.Requests.UserManagement.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvenShopfy.API.EndPoints.Identity.User;

public class EditUserInformationEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/edit-user/{userId}", Handle);

    private static async Task<IResult> Handle(
        [FromBody] UpdateUserRequest request,
        CloudinaryService cloudinaryService,
        [FromServices] UserManager<CustomUserRequest> userManager,
        string userId)
    {
        request.UserId = userId; 
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Results.BadRequest("It was not possible to find this user.");
        }
    
        if (user.UserName != request.UserName && request.UserName != null)
        {
            var findByUserName = await userManager.FindByNameAsync(request.UserName);
            if (findByUserName != null)
            {
                return Results.BadRequest("Username is already in use");
            }
        }
        
        if (user.Email != request.Email && request.Email != null)
        {
            var findByEmail = await userManager.FindByEmailAsync(request.Email);
            if (findByEmail != null)
            {
                return Results.BadRequest("Email is already in use");
            }
        }
        
        // Use UserManager to check current password
        if (!string.IsNullOrEmpty(request.PasswordHash))
        {
            var passwordCheck = await userManager.CheckPasswordAsync(user, request.PasswordHash);
            if (!passwordCheck)
            {
                return Results.BadRequest("Current password is incorrect");
            }
        
            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                var changePasswordResult =
                    await userManager.ChangePasswordAsync(user, request.PasswordHash, request.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    var passwordErrors = string.Join(", ", changePasswordResult.Errors.Select(e => e.Description));
                    return Results.BadRequest($"Password change error: {passwordErrors}");
                }
            }
        }
        
        user.Email = request.Email ?? user.Email;
        user.Gender = request.Gender ?? user.Gender;
        user.UserName = request.UserName ?? user.UserName;
        user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
        user.Name = request.Name ?? user.Name;
        
        if (request.ProfilePicture != null)
        {
            var uploadResult =
                await cloudinaryService.UploadImageAsync(request.ProfilePicture, "invenShopfy/ProfilePictures");
            user.ProfilePicture = uploadResult.SecureUrl.ToString();
        }
        
        var updateUser = await userManager.UpdateAsync(user);
        if (!updateUser.Succeeded)
        {
            var createErrors = string.Join(", ", updateUser.Errors.Select(e => e.Description));
            return Results.BadRequest($"Update user errors: {createErrors}");
        }

        return Results.Ok("User information updated successfully");
    }
}