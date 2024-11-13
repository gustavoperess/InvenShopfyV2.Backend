

namespace InvenShopfy.Core.Requests.UserManagement.User;

public class UpdateUserRequest : Request
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePicture { get; set; }
    public string? UserName { get; set; } 
    public string? Gender { get; set; }
    public string? NewPassword { get; set; }
    public string? PasswordHash { get; set; }
}