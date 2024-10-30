using InvenShopfy.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Identity;

public class IdentityUserMapping : IEntityTypeConfiguration<CustomUserRequest>
{
    public void Configure(EntityTypeBuilder<CustomUserRequest> builder)
    {
        builder.ToTable("IdentityUser");
        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.NormalizedUserName).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).IsUnique();
        
        builder.Property(u => u.Email).HasMaxLength(180);
        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.NormalizedEmail).HasMaxLength(180);
        builder.Property(u => u.UserName).HasMaxLength(180);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(180);
        builder.Property(u => u.PhoneNumber).HasMaxLength(20);
        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
        builder.Property(u => u.DateOfJoin);
        builder.Property(u => u.ProfilePicture).HasMaxLength(70000);
        builder.Property(u => u.Gender).IsRequired().HasMaxLength(10); 
        
        builder.HasMany<IdentityUserClaim<long>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
        builder.HasMany<IdentityUserLogin<long>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
        builder.HasMany<IdentityUserToken<long>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
        builder.HasMany<IdentityUserRole<long>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        
    }
}