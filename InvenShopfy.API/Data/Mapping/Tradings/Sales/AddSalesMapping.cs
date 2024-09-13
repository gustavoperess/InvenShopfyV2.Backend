using InvenShopfy.Core.Models.Tradings.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Tradings.Sales;

public class AddSalesMapping : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Purchase");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Date)
            .IsRequired(true)
            .HasColumnType("TIMESTAMPTZ");

        builder.Property(x => x.RandomNumber)
            .IsRequired(true)
            .HasColumnType("SMALLINT"); 

        builder.Property(x => x.ShippingCost)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)");
        
        builder.Property(x => x.PaymentStatus)
            .IsRequired(true)
            .HasColumnType("SMALLINT");
        
        builder.Property(x => x.SaleStatus)
            .IsRequired(true)
            .HasColumnType("SMALLINT");

        builder.Property(x => x.SaleNote)
            .IsRequired(true)
            .HasColumnType("TEXT")
            .HasMaxLength(500);
        
        builder.Property(x => x.StafNote)
            .IsRequired(true)
            .HasColumnType("TEXT")
            .HasMaxLength(500);
        
        builder.Property(x => x.Document)
            .IsRequired(true)
            .HasColumnType("VARCHAR(120)");
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
       
    }
}