namespace InvenShopfy.API.Models;

public class RolePermission 
{
    public long Id { get; set; }  
    public long RoleId { get; set; }  
    public string EntityType { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;  
    public bool IsAllowed { get; set; }  
}