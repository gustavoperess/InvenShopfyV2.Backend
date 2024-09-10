using Microsoft.AspNetCore.Identity;


namespace InvenShopfy.API.Models
{
    public class CustomIdentityRole : IdentityRole<long>
    {
    public string Description { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    }
}