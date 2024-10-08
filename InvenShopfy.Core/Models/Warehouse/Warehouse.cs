using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Models.Warehouse;

public class Warehouse
{
    public Warehouse(long id, string zipCode, string warehouseName, string phoneNumber, string email, string address, string userId)
    {
        Id = id;
        ZipCode = zipCode;
        WarehouseName = warehouseName;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        UserId = userId;
    }

    public Warehouse()
    {
    }
    
    public long Id { get; set; }
    
    private readonly ZipCode _zipCodeFormatter = new ZipCode();
    private string _zipCode = string.Empty;
    public string WarehouseName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get => _zipCode; set => _zipCode = _zipCodeFormatter.FormatZipCode(value); }
    public string UserId { get; set; } = string.Empty;
}