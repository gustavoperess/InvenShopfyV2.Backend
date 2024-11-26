using InvenShopfy.Core.Models.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Expenses;

public class ExpensePaymentMapping : IEntityTypeConfiguration<ExpensePayment>
{
    public void Configure(EntityTypeBuilder<ExpensePayment> builder)
    {
        builder.ToTable("ExpensePayment");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Date)
            .IsRequired(true)
            .HasColumnType("date");
        
        builder.Property(x => x.ExpenseNote)
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