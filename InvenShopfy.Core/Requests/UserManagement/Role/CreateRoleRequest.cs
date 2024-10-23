using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.UserManagement.Role;

public class CreateRoleRequest : Request
{
    [Required(ErrorMessage = "Invalid Role Name")]
    [MaxLength(80,  ErrorMessage= "Max len of 80 characters")]
    public string RoleName { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Description")]
    [MaxLength(256,  ErrorMessage= "Max len of 80 characters")]
    public string Description { get; set; } = null!;
    
}