using InvenShopfy.Core.Models.Warehouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Warehouses;

public class WarehouseProductMapping : IEntityTypeConfiguration<WarehouseProduct>
{
    public void Configure(EntityTypeBuilder<WarehouseProduct> builder)
    {
        builder.ToTable("WarehouseProduct");
        builder.HasKey(sp => new {  sp.ProductId, sp.WarehouseId });
    
        builder.Property(sp => sp.Quantity)
            .IsRequired(true)
            .HasColumnType("INTEGER");
        
        builder.HasOne(sp => sp.Product)
            .WithMany() 
            .HasForeignKey(sp => sp.ProductId);
    
    }
}