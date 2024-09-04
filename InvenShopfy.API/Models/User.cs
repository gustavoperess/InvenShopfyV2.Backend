using Microsoft.AspNetCore.Identity;

namespace InvenShopfy.API.Models;

public class User : IdentityUser<long>
{
    public List<IdentityRole<long>>? Roles { get; set; }
}