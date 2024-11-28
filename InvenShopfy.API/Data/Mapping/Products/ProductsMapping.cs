using InvenShopfy.Core.Models.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Products;

public class ProductsMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductName)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.ProductPrice)
            .IsRequired(true)
            .HasColumnType("MONEY");
        
        builder.Property(x => x.ProductCode)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);
        
        builder.Property(x => x.CreateAt)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.Property(x => x.ProductImage)
            .IsRequired(false)
            .HasColumnType("TEXT");
        
        builder.Property(x => x.SubCategory)
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.Property(x => x.StockQuantity)
            .IsRequired(true)
            .HasColumnType("BIGINT");
        
        builder.Property(x => x.TaxPercentage)
            .IsRequired(true)
            .HasColumnType("INT");
        
        builder.Property(x => x.Featured)
            .HasColumnType("BOOLEAN");
        
        builder.Property(x => x.MarginRange)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);
        
        
        builder.Property(x => x.Sale)
            .HasColumnType("BOOLEAN");
        
        builder.Property(x => x.Expired)
            .HasColumnType("BOOLEAN");
        
    }
}