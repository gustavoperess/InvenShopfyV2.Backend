using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Models.People;

public class Biller 
{
    public Biller() { }
    public Biller(long id, string name, DateOnly? dateOfJoin, string email, 
        string phoneNumber, string identification, string address, string country, 
        string zipCode, long billerCode, long warehouseId, string userId)
    {
        Id = id;
        Name = name;
        DateOfJoin = dateOfJoin ?? DateOnly.FromDateTime(DateTime.Now);
        Email = email;
        PhoneNumber = phoneNumber;
        Identification = identification;
        Address = address;
        Country = country;
        ZipCode = zipCode;  
        BillerCode = billerCode;
        WarehouseId = warehouseId;
        UserId = userId;
    }

    private readonly ZipCode _zipCodeFormatter = new ZipCode();
    private string _zipCode = string.Empty;
    public long Id { get; private set; }
    public string Name { get; set; } = String.Empty;
    public DateOnly DateOfJoin { get; set; }
    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Identification { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ZipCode { get => _zipCode; 
        set => _zipCode = _zipCodeFormatter.FormatZipCode(value); }
    public long BillerCode { get; set; } 
    
    public long WarehouseId { get; set; }
    public Warehouse.Warehouse Warehouse { get; set; } = null!;
    
    public string UserId { get; set; } = string.Empty;
    

}

