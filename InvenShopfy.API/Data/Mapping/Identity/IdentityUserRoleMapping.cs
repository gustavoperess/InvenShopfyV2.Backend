using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Identity;

// public class IdentityUserRoleMapping
//     : IEntityTypeConfiguration<IdentityUserRole<long>>
// {
//     public void Configure(EntityTypeBuilder<IdentityUserRole<long>> builder)
//     {
//         builder.ToTable("IdentityUserRole");
//         builder.HasKey(r => r.UserId);
//         
//         builder.HasOne<CustomUserRequest>()
//             .WithOne() 
//             .HasForeignKey<IdentityUserRole<long>>(ur => ur.UserId)
//             .IsRequired();
//         
//         builder.HasOne<CustomIdentityRole>()
//             .WithMany() 
//             .HasForeignKey(ur => ur.RoleId)
//             .IsRequired();
//     }
// }

public class IdentityUserRoleMapping : IEntityTypeConfiguration<IdentityUserRole<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<long>> builder)
    {
        builder.ToTable("IdentityUserRole");
        
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });
        
        builder.HasOne<CustomUserRequest>()
            .WithMany()
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.HasOne<CustomIdentityRole>()
            .WithMany()
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();
    }
}
