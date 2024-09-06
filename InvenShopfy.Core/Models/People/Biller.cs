namespace InvenShopfy.Core.Models.People;

public class Biller
{
    public long Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public DateTime DateOfJoin { get; set; }
    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Identification { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public double BillerCode { get; set; } 
    public string UserId { get; set; } = string.Empty;
    
    public long WarehouseId { get; set; }
    public Warehouse.Warehouse Warehouse { get; set; } = null!;

}