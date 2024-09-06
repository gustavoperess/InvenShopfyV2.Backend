using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Models.Warehouse;

public class Warehouse
{
    private readonly ZipCode _zipCodeFormatter = new ZipCode();
    private string _zipCode = string.Empty;
    public long Id { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get => _zipCode; set => _zipCode = _zipCodeFormatter.FormatZipCode(value); }
    public string UserId { get; set; } = string.Empty;
}