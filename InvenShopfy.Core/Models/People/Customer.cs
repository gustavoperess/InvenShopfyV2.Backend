
namespace InvenShopfy.Core.Models.People;



public class Customer
{
    
    public long Id { get; init; }
    public string CustomerName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public long? RewardPoint { get; set; }
    public string CustomerGroup { get; set; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    
}