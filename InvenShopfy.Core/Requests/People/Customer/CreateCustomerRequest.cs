using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Enum;
using InvenShopfy.Core.Standards;

namespace InvenShopfy.Core.Requests.People.Customer;

public class CreateCustomerRequest : Request
{
    private readonly ZipCode _zipCodeFormatter = new ZipCode();
    
    [Required(ErrorMessage = "Invalid Name")]
    [MaxLength(150, ErrorMessage = "Max length of 150 characters")]
    public string CustomerName { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Invalid Email")]
    [EmailAddress]
    [MaxLength(160, ErrorMessage = "Max length of 160 characters")]
    public string Email { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Mobile number is required")]
    [RegularExpression(@"^\(\d{3}\)\s\d{3}-\d{4}$", ErrorMessage = "Please enter a valid phone number in the format (123) 456-7890.")]
    [MaxLength(16, ErrorMessage = "Max length of 16 characters")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid City")]
    [MaxLength(80, ErrorMessage = "Max length of 80 characters")]
    public string City { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Country")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Country { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(160, ErrorMessage = "Max length of 160 characters")]
    public string Address { get; set; } = string.Empty;
    
    private string _zipCode = string.Empty;
    [Required(ErrorMessage = "Invalid Zip Code")]
    public string ZipCode
    {
        get => _zipCode;
        set => _zipCode = _zipCodeFormatter.FormatZipCode(value);
    }   
    public long? RewardPoint { get; set; }
    
    [Required(ErrorMessage = "Please Select one of the Customers")]
    [AllowedValues("General", "Walk In", "Local", "Foreign",
        ErrorMessage = "Please select one of the allowed values General, WalkIn, Local, Foreign")]
    public string CustomerGroup { get; set; } = ECustomerGroup.WalkIn.ToString();
    
}