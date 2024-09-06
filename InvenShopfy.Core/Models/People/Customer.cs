namespace InvenShopfy.Core.Models.People;


public enum CustomerGroup
{
    General,
    WalkIn,
    Local,
    Foreign
}

public class Customer
{
    public long Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string RewardPoint { get; set; } = string.Empty;
    public CustomerGroup CustomerGroup { get; set; }
    public string UserId { get; set; } = string.Empty;
}