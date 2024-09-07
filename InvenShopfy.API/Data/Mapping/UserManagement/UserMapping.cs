using InvenShopfy.Core.Models.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.UserManagement;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(150);
        
        builder.Property(x => x.DateOfJoin)
            .IsRequired(true)
            .HasColumnType("TIMESTAMPTZ");
        
        builder.Property(x => x.Email)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.PhoneNumber)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Gender)
            .IsRequired(true)
            .HasColumnType("VARCHAR");
        
        builder.Property(x => x.Username)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.ProfileImage)
            .IsRequired(false)
            .HasColumnType("VARCHAR");

        builder.Property(x => x.Password)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}



