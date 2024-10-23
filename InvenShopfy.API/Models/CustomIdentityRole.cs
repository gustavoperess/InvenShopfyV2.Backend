using Microsoft.AspNetCore.Identity;


namespace InvenShopfy.API.Models
{
    public class CustomIdentityRole : IdentityRole<long>
    {
        public string Description { get; set; } = String.Empty;
    }
}