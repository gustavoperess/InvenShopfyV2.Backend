using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Models.People;



public class Customer
{
    public long Id { get; private set; }
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public long RewardPoint { get; set; }
    public ECustomerGroup CustomerGroup { get; set; } = ECustomerGroup.WalkIn;
    public string UserId { get; set; } = string.Empty;
}