using InvenShopfy.Core.Enum;
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
            .HasColumnType("date");
        
        builder.Property(x => x.ExpenseDescription)
            .IsRequired(true)
            .HasColumnType("VARCHAR(50)");
        
        builder.Property(x => x.ExpenseType)
            .IsRequired(true)
            .HasColumnType("VARCHAR");

        builder.Property(x => x.VoucherNumber)
            .IsRequired(true)
            .HasColumnType("VARCHAR");
        
        builder.Property(x => x.ShippingCost)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)");

        builder.Property(x => x.ExpenseCost)
            .IsRequired(true)
            .HasColumnType("NUMERIC(18,2)");
        
        builder.Property(x => x.ExpenseNote)
            .IsRequired(true)
            .HasColumnType("TEXT")
            .HasMaxLength(500);
        
        builder.Property(x => x.ExpenseStatus)
            .IsRequired(true)
            .HasColumnType("VARCHAR(50)"); 
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}
