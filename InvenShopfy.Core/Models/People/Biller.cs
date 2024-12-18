using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Models.People;

public class Biller 
{

    private readonly ZipCode _zipCodeFormatter = new ZipCode();
    private string _zipCode = string.Empty;
    public long Id { get; init; }
    public string Name { get; set; } = String.Empty;
    public DateOnly DateOfJoin { get; init; }
    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Identification { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ZipCode { get => _zipCode; 
        set => _zipCode = _zipCodeFormatter.FormatZipCode(value); }
    public long BillerCode { get; set; } 
    
    public long WarehouseId { get; set; }
    public Warehouse.Warehouse Warehouse { get; init; } = null!;
    
    public string UserId { get; set; } = string.Empty;
    

}

