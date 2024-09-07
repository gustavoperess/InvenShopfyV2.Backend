using InvenShopfy.Core.Models.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Products;


public class UnitMapping : IEntityTypeConfiguration<Unit>
{
    public  void Configure(EntityTypeBuilder<Unit> builder)
    {
        
        builder.ToTable("Unit");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.ShortName)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
    }
}