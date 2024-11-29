namespace InvenShopfy.API.Models;

public class RolePermissionRequest
{
    public string RoleName { get; set; } = string.Empty;  
    public List<RolePermissionDto> Permissions { get; set; } = new(); 
}