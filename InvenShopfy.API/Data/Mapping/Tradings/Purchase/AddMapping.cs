using InvenShopfy.Core.Models.Tradings.Purchase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Tradings.Purchase;

public class AddMapping : IEntityTypeConfiguration<Add>
{
    public void Configure(EntityTypeBuilder<Add> builder)
    {
        builder.ToTable("Purchase");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Date)
            .IsRequired(true)
            .HasColumnType("TIMESTAMPTZ");

        builder.Property(x => x.PurchaseStatus)
            .IsRequired(true)
            .IsRequired(true)
            .HasColumnType("SMALLINT"); 

        builder.Property(x => x.ShippingCost)
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
        
       
    }
}