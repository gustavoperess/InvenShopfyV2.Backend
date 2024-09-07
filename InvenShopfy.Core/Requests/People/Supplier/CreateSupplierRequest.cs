using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Requests.People.Supplier;

public class CreateSupplierRequest : Request
{
    private readonly ZipCode _zipCodeFormatter = new ZipCode();
    
    [Required(ErrorMessage = "Invalid Name")]
    [MaxLength(150, ErrorMessage = "Max length of 150 characters")]
    public string Name { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Phone Phone Number")]
    [MaxLength(80, ErrorMessage = "Max length of 80 characters")]
    public string PhoneNumber { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Email")]
    [MaxLength(150, ErrorMessage = "Max length of 150 characters")]
    public string Email { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Supplier Code")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public long SupplierCode { get; set; }
    
    [Required(ErrorMessage = "Invalid Country")]
    [MaxLength(80, ErrorMessage = "Max length of 80 characters")]
    public string Country { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid City")]
    [MaxLength(80, ErrorMessage = "Max length of 80 characters")]
    public string City { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(160, ErrorMessage = "Max length of 160 characters")]
    public string Address { get; set; } = String.Empty;
    
    private string _zipCode = string.Empty;
    [Required(ErrorMessage = "Invalid Zip Code")]
    [MaxLength(20, ErrorMessage = "Max length of 20 characters")]
    public string ZipCode
    {
        get => _zipCode;
        set => _zipCode = _zipCodeFormatter.FormatZipCode(value);
    }   
    
    
    [Required(ErrorMessage = "Invalid Company")]
    [MaxLength(160, ErrorMessage = "Max length of 160 characters")]
    public string Company { get; set; } = String.Empty;

}