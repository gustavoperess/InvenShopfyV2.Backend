using Microsoft.AspNetCore.Identity;
using InvenShopfy.Core.Enum;

namespace InvenShopfy.API.Models;

public class CustomUserRequest : IdentityUser<long>
{
    public string Name { get; set; } = null!;
    public DateTime? DateOfJoin { get; set; } = DateTime.UtcNow;
    public string? ProfilePicture { get; set; }
    public string Gender { get; set; } = EGender.Male.ToString();
    // public List<IdentityUserRole<long>>? UserRoles { get; set; } = new List<IdentityUserRole<long>>();
    
    public long RoleId { get; set; }
    
    // public ICollection<IdentityUserClaim<long>> UserClaims { get; set; } = new List<IdentityUserClaim<long>>();
    // public ICollection<IdentityUserRole<long>> UserRoles { get; set; } = new List<IdentityUserRole<long>>();

    public virtual DateTime? LastActivityTime { get; set; }
    
}