using Microsoft.AspNetCore.Identity;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.API.Models;

public class User : IdentityUser<long>
{
    public List<IdentityRole<long>>? Roles { get; set; }
    public DateTime DateOfJoin { get; set; } = DateTime.UtcNow;
    public EGender Gender { get; set; } = EGender.Male;
}