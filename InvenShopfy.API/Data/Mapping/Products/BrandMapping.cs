using InvenShopfy.Core.Models.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Products;

public class BrandMapping : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brand");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.BrandImage)
            .IsRequired(false)
            .HasColumnType("TEXT");
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}