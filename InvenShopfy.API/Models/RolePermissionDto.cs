namespace InvenShopfy.API.Models;

public class RolePermissionDto
{
    public string EntityType { get; set; } = string.Empty;  
    public string Action { get; set; } = string.Empty;      
    public bool IsAllowed { get; set; }                   
}