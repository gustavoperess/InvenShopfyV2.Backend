using InvenShopfy.Core.Models.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.UserManagement;

public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.RoleName)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(150);
        
        builder.Property(x => x.Description)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.ConcurrencyStamp)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}
