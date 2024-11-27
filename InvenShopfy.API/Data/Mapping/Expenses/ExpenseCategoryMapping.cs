using InvenShopfy.Core.Models.Expenses;
using InvenShopfy.Core.Models.Expenses.ExpenseCategory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Expenses;

public class ExpenseCategoryMapping : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        builder.ToTable("ExpenseCategory");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.MainCategory)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(180);

        builder.Property(x => x.SubCategory)
            .IsRequired(true)
            .HasColumnType("text[]");
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}