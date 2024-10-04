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
            .IsRequired(true)
            .HasColumnType("TIMESTAMP");

        builder.Property(x => x.PurchaseStatus)
            .IsRequired(true)
            .IsRequired(true)
            .HasColumnType("VARCHAR(50)"); 

        builder.Property(x => x.ShippingCost)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)")
            .HasMaxLength(80);
        
        builder.Property(x => x.TotalQuantityBought)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)")
            .HasMaxLength(80);

        builder.Property(x => x.PurchaseNote)
            .IsRequired(true)
            .HasColumnType("TEXT")
            .HasMaxLength(500);
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.ReferenceNumber)
            .IsRequired(true)
            .HasColumnType("VARCHAR");
        
       
    }
}