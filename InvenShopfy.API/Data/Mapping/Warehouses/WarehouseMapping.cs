using System.Text.Json;
using InvenShopfy.Core.Models.Warehouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Warehouses;

public class WarehouseMapping : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouse");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.WarehouseName)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);

        builder.Property(x => x.WarehousePhoneNumber)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.WarehouseEmail)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.WarehouseCity)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.WarehouseZipCode)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

        builder.Property(x => x.WarehouseCountry)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.WarehouseOpeningNotes)
            .IsRequired(false)
            .HasColumnType("TEXT");

        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}