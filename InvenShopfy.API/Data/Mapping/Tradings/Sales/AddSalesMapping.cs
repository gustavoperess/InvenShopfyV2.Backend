using InvenShopfy.Core.Models.Tradings.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Tradings.Sales;

public class AddSalesMapping : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SaleDate)
            .IsRequired(true)
            .HasColumnType("date");

        builder.Property(x => x.ReferenceNumber)
            .IsRequired(true)
            .HasColumnType("VARCHAR");

        builder.Property(x => x.ShippingCost)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)");
        
        builder.Property(x => x.Discount)
            .IsRequired(true)
            .HasColumnType("INT");
        
        builder.Property(x => x.TaxAmount)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)");

        builder.Property(x => x.ProfitAmount)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)");
        
        builder.Property(x => x.SaleStatus)
            .IsRequired(true)
            .HasColumnType("VARCHAR(50)"); 

        builder.Property(x => x.SaleNote)
            .IsRequired(true)
            .HasColumnType("TEXT")
            .HasMaxLength(500);

        builder.Property(x => x.StaffNote)
            .IsRequired(true)
            .HasColumnType("TEXT")
            .HasMaxLength(500);
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.TotalAmount)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)");
    }
}