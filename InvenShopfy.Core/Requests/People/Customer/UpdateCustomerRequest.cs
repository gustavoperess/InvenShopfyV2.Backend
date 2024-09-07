using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Models.People;
using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Requests.People.Customer;

public class UpdateCustomerRequest : Request
{
    public long Id { get; set; }
    private readonly ZipCode _zipCodeFormatter = new ZipCode();
    
    [Required(ErrorMessage = "Invalid Name")]
    [MaxLength(150, ErrorMessage = "Max length of 150 characters")]
    public string Name { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Email")]
    [MaxLength(150, ErrorMessage = "Max length of 150 characters")]
    public string Email { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Phone Number")]
    [MaxLength(80, ErrorMessage = "Max length of 80 characters")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid City")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string City { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Country")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Country { get; set; } = string.Empty;
    
    
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(150, ErrorMessage = "Max length of 150 characters")]
    public string Address { get; set; } = string.Empty;
    
    private string _zipCode = string.Empty;
    [Required(ErrorMessage = "Invalid Zip Code")]
    public string ZipCode
    {
        get => _zipCode;
        set => _zipCode = _zipCodeFormatter.FormatZipCode(value);
    }   
    
    [Required(ErrorMessage = "Invalid Rewards Point")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string RewardPoint { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Please Select one of the Customers")]
    public CustomerGroup CustomerGroup { get; set; }
}