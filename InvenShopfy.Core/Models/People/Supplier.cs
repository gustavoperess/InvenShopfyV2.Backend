namespace InvenShopfy.Core.Models.People;

public class Supplier
{
    public long Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public double SupplierCode { get; set; }
    public string Country { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public string ZipCode { get; set; } = String.Empty;
    public string Company { get; set; } = String.Empty;
    public string UserId { get; set; } = string.Empty;
    
}