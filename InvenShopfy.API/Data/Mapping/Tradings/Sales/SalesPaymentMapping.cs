using InvenShopfy.Core.Models.Tradings.Sales.SalesPayment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Tradings.Sales;

public class SalesPaymentMapping : IEntityTypeConfiguration<SalesPayment>
{
    public void Configure(EntityTypeBuilder<SalesPayment> builder)
    {
        builder.ToTable("SalesPayment");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Date)
            .IsRequired(true)
            .HasColumnType("date");
        
        builder.Property(x => x.SalesNote)
            .IsRequired(true)
            .HasColumnType("TEXT")
            .HasMaxLength(500);
        
        builder.Property(x => x.CardNumber)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(30);
        
        builder.Property(x => x.PaymentType)
            .IsRequired(true)
            .HasColumnType("VARCHAR(50)"); 
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}