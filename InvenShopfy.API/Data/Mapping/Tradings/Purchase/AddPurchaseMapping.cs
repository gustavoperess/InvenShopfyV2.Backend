using InvenShopfy.Core.Models.Tradings.Purchase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Tradings.Purchase;

public class AddPurchaseMapping : IEntityTypeConfiguration<AddPurchase>
{
    public void Configure(EntityTypeBuilder<AddPurchase> builder)
    {
        builder.ToTable("Purchase");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.PurchaseDate)
            .HasColumnType("date");

        builder.Property(x => x.PurchaseStatus)
            .HasColumnType("VARCHAR(50)"); 

        builder.Property(x => x.ShippingCost)
            .HasColumnType("NUMERIC(18,2)")
            .HasMaxLength(80);
        
        builder.Property(x => x.TotalAmountBought)
            .HasColumnType("NUMERIC(18,2)")
            .HasMaxLength(80);

        builder.Property(x => x.PurchaseNote)
            .IsRequired(true) // same parameter as the default
            .HasColumnType("TEXT")
            .HasMaxLength(500);
        
        builder.Property(x => x.HasProductBeenReturnn)
            .HasColumnType("BOOLEAN");
        
        builder.Property(bp => bp.TotalTax)
            .HasColumnType("NUMERIC(18,2)");
        
        builder.Property(x => x.UserId)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.ReferenceNumber)
            .HasColumnType("VARCHAR");
        
        builder.Property(x => x.TotalNumberOfProductsBought)
            .HasColumnType("BIGINT");
    }
}