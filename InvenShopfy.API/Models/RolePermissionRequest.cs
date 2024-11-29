
using InvenShopfy.Core.Requests;

namespace InvenShopfy.API.Models;

public class RolePermissionRequest : Request
{
    public string RoleName { get; set; } = string.Empty;  
    public List<RolePermissionDto> Permissions { get; set; } = new(); 
}