namespace InvenShopfy.Core.Models.UserManagement;

public class Role
{
    public long Id { get; set; }
    public string RoleName { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string UserId { get; set; } = string.Empty;
}