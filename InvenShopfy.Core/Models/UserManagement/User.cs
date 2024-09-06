namespace InvenShopfy.Core.Models.UserManagement;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public DateTime DateOfJoin { get; set; } = DateTime.Now;
    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public List<string> Gender { get; set; } = new List<string> { "Male", "Female" };
    public string Username { get; set; } = string.Empty;
    public string ProfileImage { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public long RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
