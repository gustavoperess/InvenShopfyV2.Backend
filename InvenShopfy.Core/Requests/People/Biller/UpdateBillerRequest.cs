using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Requests.People.Biller;

public class UpdateBillerRequest : Request
{
    public long Id { get; set; }
    private readonly ZipCode _zipCodeFormatter = new ZipCode();
    
    [Required(ErrorMessage = "Invalid Voucher Name")]
    [MaxLength(150, ErrorMessage = "Max length of 150 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Invalid Email Address")]
    [MaxLength(150, ErrorMessage = "Max length of 150 characters")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Invalid Phone Number")]
    [MaxLength(80, ErrorMessage = "Max length of 80 characters")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Invalid Identification")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Identification { get; set; } = string.Empty;

    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Invalid Country")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Country { get; set; } = string.Empty;
    
    private string _zipCode = string.Empty;
    [Required(ErrorMessage = "Invalid Zip Code")]
    public string ZipCode
    {
        get => _zipCode;
        set => _zipCode = _zipCodeFormatter.FormatZipCode(value);
    }
    
    [Required(ErrorMessage = "Invalid Biller Code")]
    [Range(0, 9999999999, ErrorMessage = "Biller Code must be between 0 and 9999999999")]
    public double BillerCode { get; set; }

    [Required(ErrorMessage = "Invalid Warehouse")]
    public long WarehouseId { get; set; }
}