using InvenShopfy.Core.Enum;

namespace InvenShopfy.Core.Models.People;



public class Customer
{

    public Customer() {}

    public Customer(long id,string name, string email, string phoneNumber, string city, string county,
        string address, string zipCode, long rewardPoint, string customerGroup, string userId)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        City = city;
        Country = county;
        Address = address;
        ZipCode = zipCode;
        RewardPoint = rewardPoint;
        CustomerGroup = customerGroup;
        UserId = userId;
    }
    
    public long Id { get; private set; }
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public long RewardPoint { get; set; }
    public string CustomerGroup { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    
}