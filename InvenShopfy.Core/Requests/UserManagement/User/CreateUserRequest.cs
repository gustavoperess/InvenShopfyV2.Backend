using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Models.UserManagement;

namespace InvenShopfy.Core.Requests.UserManagement.User;

public class CreateUserRequest : Request
{
    [Required(ErrorMessage = "Invalid Name")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string Name { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Email")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string Email { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Phone Number")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Please select the Gender from the dropdown below")]
    public Gender Gender { get; set; }
    
    [Required(ErrorMessage = "Invalid Username")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string Username { get; set; } = string.Empty;
    
    // [Required(ErrorMessage = "Please update a profile Image")]
    public string ProfileImage { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Please add a password")]
    [MaxLength(160,  ErrorMessage= "Max len of 160 characters")]
    public string Password { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Role ID")]
    public long RoleId { get; set; }
}