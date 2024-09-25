using InvenShopfy.Core.Models.Tradings.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Tradings.Sales;

public class SaleProductMapping : IEntityTypeConfiguration<SaleProduct>
{
    public void Configure(EntityTypeBuilder<SaleProduct> builder)
    {
        builder.ToTable("SaleProduct");
        builder.HasKey(sp => new { sp.SaleId, sp.ProductId });

        builder.Property(sp => sp.TotalQuantitySoldPerProduct)
            .IsRequired(true)
            .HasColumnType("INTEGER");

        builder.Property(sp => sp.TotalPricePerProduct)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)");
    }
}