using InvenShopfy.Core.Models.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.People;

public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Biller");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(150);
        
        builder.Property(x => x.Email)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.PhoneNumber)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(30);
        
        builder.Property(x => x.City)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Country)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Address)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.ZipCode)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);
        
        builder.Property(x => x.RewardPoint)
            .IsRequired(true)
            .HasColumnType("BIGINT")
            .HasMaxLength(30);

        builder.Property(x => x.CustomerGroup)
            .IsRequired(true)
            .HasColumnType("VARCHAR");
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}

