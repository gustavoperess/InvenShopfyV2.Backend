using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.People.Supplier;

public class UpdateSupplierRequest : Request
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Invalid Name")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Name { get; set; } = String.Empty;
    [Required(ErrorMessage = "Invalid Phone Number")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string PhoneNumber { get; set; } = String.Empty;
    [Required(ErrorMessage = "Invalid Email")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Email { get; set; } = String.Empty;
    [Required(ErrorMessage = "Invalid Supplier Code")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public double SupplierCode { get; set; }
    [Required(ErrorMessage = "Invalid Country")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Country { get; set; } = String.Empty;
    [Required(ErrorMessage = "Invalid City")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string City { get; set; } = String.Empty;
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Address { get; set; } = String.Empty;
    [Required(ErrorMessage = "Invalid Zip Code")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string ZipCode { get; set; } = String.Empty;
    [Required(ErrorMessage = "Invalid Company")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Company { get; set; } = String.Empty;
}