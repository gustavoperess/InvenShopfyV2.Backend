namespace InvenShopfy.Core.Models.UserManagement;

public class Role
{
    public long Id { get; init; }
    public string RoleName { get; init; } = String.Empty;
    public string Description { get; init; } = String.Empty;
    public string UserId { get; init; } = string.Empty;
}