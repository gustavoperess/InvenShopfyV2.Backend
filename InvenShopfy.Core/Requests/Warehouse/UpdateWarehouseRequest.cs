using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Requests.Warehouse;

public class UpdateWarehouseRequest : Request
{
    private readonly ZipCode _zipCodeFormatter = new ZipCode();
    public long Id { get; set; }
    [Required(ErrorMessage = "Invalid Warehouse Name")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string WarehouseName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Phone Number")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Email")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Address { get; set; } = string.Empty;
    
    private string _zipCode = string.Empty;
    [Required(ErrorMessage = "Invalid Zip Code")]
    public string ZipCode
    {
        get => _zipCode;
        set => _zipCode = _zipCodeFormatter.FormatZipCode(value);
    }   
}