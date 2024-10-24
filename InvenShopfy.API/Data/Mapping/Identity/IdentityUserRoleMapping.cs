using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Identity;

public class IdentityUserRoleMapping
    : IEntityTypeConfiguration<IdentityUserRole<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<long>> builder)
    {
        builder.ToTable("IdentityUserRole");
        builder.HasKey(r => new { r.UserId, r.RoleId });
        
        builder.HasOne<CustomUserRequest>()
            .WithMany(u => u.UserRoles) // Each user can have many roles
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
        
        builder.HasOne<CustomIdentityRole>()
            .WithMany() // Roles have no navigation property back to users
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();
    }
}