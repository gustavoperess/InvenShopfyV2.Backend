namespace InvenShopfy.Core.Models.People;

public class BillerDto
{
    public long Id { get; init; }
    public string Name { get; set; } = String.Empty;
    public DateOnly DateOfJoin { get; init; }
    public string WarehouseName { get; init; } = null!;
    public string ZipCode { get; init; } = null!;
    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Identification { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public long BillerCode { get; set; } 
    
}