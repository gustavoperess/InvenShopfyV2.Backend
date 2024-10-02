using InvenShopfy.Core.Models.Tradings.Purchase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Tradings.Purchase;

public class PurchaseProductMapping : IEntityTypeConfiguration<PurchaseProduct>
{
    public void Configure(EntityTypeBuilder<PurchaseProduct> builder)
    {
        builder.ToTable("PurchaseProduct");
        builder.HasKey(sp => new { sp.PurchaseId, sp.ProductId });

        builder.Property(sp => sp.TotalQuantityBoughtPerProduct)
            .IsRequired(true)
            .HasColumnType("INTEGER");

        builder.Property(sp => sp.TotalPricePaidPerProduct)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)");
    }
}