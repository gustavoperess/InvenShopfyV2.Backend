using InvenShopfy.Core.Models.Tradings.Returns.SalesReturn;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Tradings.Returns;

public class AddSalesReturnMapping : IEntityTypeConfiguration<SaleReturn>
{
    public void Configure(EntityTypeBuilder<SaleReturn> builder)
    {
        builder.ToTable("SalesReturn");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ReferenceNumber)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.BillerName)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);
              
        builder.Property(x => x.CustomerName)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);
              
        builder.Property(x => x.WarehouseName)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);
        
        builder.Property(x => x.RemarkStatus)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);
        
        builder.Property(x => x.ReturnTotalAmount)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)");
        
        builder.Property(x => x.ReturnNote)
            .IsRequired(true)
            .HasColumnType("TEXT");
        
        builder.Property(x => x.ReturnDate)
            .IsRequired(true)
            .HasColumnType("date");
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}