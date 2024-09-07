using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Models.People;

public class Supplier
{
    private readonly ZipCode _zipCodeFormatter = new ZipCode();
    private string _zipCode = string.Empty;
    public long Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public long SupplierCode { get; set; }
    public string Country { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public string ZipCode { get => _zipCode; set => _zipCode = _zipCodeFormatter.FormatZipCode(value); }
    public string Company { get; set; } = String.Empty;
    public string UserId { get; set; } = string.Empty;
    
}