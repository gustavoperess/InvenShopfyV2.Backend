using Microsoft.AspNetCore.Identity;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.API.Models;

public class CustomUserRequest : IdentityUser<long>
{
    public string Name { get; set; } = null!;
    
    public string RoleName { get; set; } = null!;
    
    public List<IdentityUserRole<long>>? UserRoles { get; set; } = new List<IdentityUserRole<long>>();

    public DateTime DateOfJoin { get; set; } = DateTime.UtcNow;
    public string Gender { get; set; } = EGender.Male.ToString();
}