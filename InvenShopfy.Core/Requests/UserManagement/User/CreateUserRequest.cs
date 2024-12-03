using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.UserManagement.User;

public class CreateUserRequest 
{
    [Required(ErrorMessage = "Invalid Name")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Email")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Phone Number")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string PhoneNumber { get; set; } = string.Empty;
    public string? ProfilePicture { get; set; }
    
    [Required(ErrorMessage = "Invalid Role Name")]
    public long RoleId { get; set; }
    
    [Required(ErrorMessage = "Invalid Username")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string UserName { get; set; } = string.Empty;
    public string Gender { get; set; }  = string.Empty;
    
    [Required(ErrorMessage = "Please add a password")]
    [MaxLength(160,  ErrorMessage= "Max len of 160 characters")]
    public string PasswordHash { get; set; } = string.Empty;
    
}