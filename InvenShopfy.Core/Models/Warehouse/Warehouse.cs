namespace InvenShopfy.Core.Models.Warehouse;

public class Warehouse
{
    public long Id { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}