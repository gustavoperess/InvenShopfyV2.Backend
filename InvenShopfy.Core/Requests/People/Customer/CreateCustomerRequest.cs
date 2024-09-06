using System.ComponentModel.DataAnnotations;
using InvenShopfy.Core.Models.People;

namespace InvenShopfy.Core.Requests.People.Customer;

public class CreateCustomerRequest : Request
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
    
    [Required(ErrorMessage = "Please Select one of the Customers")]
    public CustomerGroup CustomerGroup { get; set; }
 
}