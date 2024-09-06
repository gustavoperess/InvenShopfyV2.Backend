using System.ComponentModel.DataAnnotations;

namespace InvenShopfy.Core.Requests.People.Customer;

public class CreateCustomerRequest
{
    
    [Required(ErrorMessage = "Invalid Name")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Name { get; set; } = String.Empty;
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Email { get; set; } = String.Empty;
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string City { get; set; } = string.Empty;
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Country { get; set; } = string.Empty;
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string Address { get; set; } = string.Empty;
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string ZipCode { get; set; } = string.Empty;
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public string RewardPoint { get; set; } = string.Empty;
    [Required(ErrorMessage = "Invalid Address")]
    [MaxLength(30, ErrorMessage = "Max length of 30 characters")]
    public List<string> CustomerGroup { get; set; } = new List<string> { "General", "Walk in", "Local", "Foreign" };
 
}