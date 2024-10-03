using InvenShopfy.Core.Models.Tradings.Purchase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Tradings.Purchase;

public class PurchaseProductMapping : IEntityTypeConfiguration<PurchaseProduct>
{
    public void Configure(EntityTypeBuilder<PurchaseProduct> builder)
    {
        builder.ToTable("PurchaseProduct");
        builder.HasKey(bp => new { bp.AddPurchaseId, bp.ProductId });

        builder.Property(bp => bp.TotalQuantityBoughtPerProduct)
            .IsRequired(true)
            .HasColumnType("INTEGER");

        builder.Property(bp => bp.TotalPricePaidPerProduct)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)");
    }
}