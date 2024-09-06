using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.People.Biller;

public class CreateBillerRequest : Request
{
    [Required(ErrorMessage = "Invalid Voucher Name")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Invalid Email Address")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Invalid Phone Number")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
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

    [Required(ErrorMessage = "Invalid Zip Code")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string ZipCode { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Biller Code")]
    [Range(0, 9999999999, ErrorMessage = "Biller Code must be between 0 and 9999999999")]
    public double BillerCode { get; set; }

    [Required(ErrorMessage = "Invalid Warehouse")]
    public long WarehouseId { get; set; }
}
