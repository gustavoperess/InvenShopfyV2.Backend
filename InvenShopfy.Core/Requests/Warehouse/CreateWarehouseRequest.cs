using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Requests.Warehouse;

public class CreateWarehouseRequest : Request
{
    private readonly ZipCode _zipCodeFormatter = new ZipCode();
    
    [Required(ErrorMessage = "Invalid Warehouse Name")]
    [MaxLength(50, ErrorMessage = "Max length of 50 characters")]
    public string WarehouseName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Phone Number")]
    [MaxLength(50, ErrorMessage = "Max length of 50 characters")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Email")]
    [MaxLength(160, ErrorMessage = "Max length of 160 characters")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(160, ErrorMessage = "Max length of 160 characters")]
    public string Address { get; set; } = string.Empty;
    
    private string _zipCode = string.Empty;
    [Required(ErrorMessage = "Invalid Zip Code")]
    [MaxLength(20, ErrorMessage = "Max length of 20 characters")]
    public string ZipCode
    {
        get => _zipCode;
        set => _zipCode = _zipCodeFormatter.FormatZipCode(value);
    }   

}