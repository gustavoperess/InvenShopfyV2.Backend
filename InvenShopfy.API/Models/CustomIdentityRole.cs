using Microsoft.AspNetCore.Identity;


namespace InvenShopfy.API.Models
{
    public class CustomIdentityRole : IdentityRole<long>
    {
        public string Description { get; set; } = String.Empty;
        
        // public ICollection<CustomUserRequest> Users { get; set; } = new List<CustomUserRequest>();
    }
}