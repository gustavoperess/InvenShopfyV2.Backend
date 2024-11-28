using Microsoft.AspNetCore.Identity;


namespace InvenShopfy.API.Models
{
    public class CustomIdentityRole : IdentityRole<long>
    {
        public string Description { get; set; } = String.Empty;

        // public Dictionary<string, bool> Product { get; set; } = new Dictionary<string, bool>
        // {
        //     {"add" , true},
        //     {"delete" , true},
        //     {"update" , true},
        // };
    }
}