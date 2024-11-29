using InvenShopfy.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Identity;

public class RolePermissionMapping 
    : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions"); // Name of the table
        builder.HasKey(rp => rp.Id); // Primary Key

        builder.Property(rp => rp.EntityType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(rp => rp.Action)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(rp => rp.IsAllowed)
            .IsRequired();

        builder.HasOne<CustomIdentityRole>() // Foreign Key Relationship
            .WithMany() // One role can have many permissions
            .HasForeignKey(rp => rp.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}