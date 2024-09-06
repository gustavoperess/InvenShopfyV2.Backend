using InvenShopfy.Core.Models.Expenses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Expenses;

public class ExpenseMapping : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expense");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Date)
            .IsRequired(true)
            .HasColumnType("TIMESTAMPTZ");
        
        builder.Property(x => x.WarehouseId)
            .IsRequired(false)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.ExpenseType)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
            
        builder.Property(x => x.ExpenseCategoryId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
            
        builder.Property(x => x.VoucherNumber)
            .IsRequired(true)
            .HasColumnType("BIGINT(18,2)")
            .HasMaxLength(160);

        builder.Property(x => x.Amount)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)");
        
        builder.Property(x => x.PurchaceNote)
            .IsRequired(true)
            .HasColumnType("TEXT")
            .HasMaxLength(500);
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}
