using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Models.UserManagement;


public class User
{
    public long Id { get; init; }
    public string Name { get; init; } = String.Empty;
    public DateOnly DateOfJoin { get; init; } = DateOnly.FromDateTime(DateTime.Now);
    public string Email { get; init; } = String.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Gender { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string ProfileImage { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    
    public long RoleId { get; init; }
    public Role Role { get; init; } = null!;
    
}
