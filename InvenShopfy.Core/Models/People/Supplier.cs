using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Models.People;

public class Supplier
{
    private readonly ZipCode _zipCodeFormatter = new ZipCode();
    private string _zipCode = string.Empty;
    public long Id { get; init; }
    public string SupplierName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public long SupplierCode { get; set; }
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get => _zipCode; set => _zipCode = _zipCodeFormatter.FormatZipCode(value); }
    public string Company { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    
}