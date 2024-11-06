using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Transfer
{
    public class TransferMapping : IEntityTypeConfiguration<Core.Models.Transfer.Transfer>
    {
        public void Configure(EntityTypeBuilder<Core.Models.Transfer.Transfer> builder)
        {
            builder.ToTable("Transfer");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductName)
                .IsRequired(true)
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.TransferDate)
                .IsRequired(true)
                .HasColumnType("date");

            builder.Property(x => x.ReferenceNumber)
                .IsRequired(true)
                .HasColumnType("VARCHAR");

            builder.Property(x => x.Quantity)
                .IsRequired(true)
                .HasColumnType("INT");

            builder.Property(x => x.AuthorizedBy)
                .IsRequired(true)
                .HasColumnType("VARCHAR");

            builder.Property(x => x.Reason)
                .IsRequired(true)
                .HasColumnType("VARCHAR")
                .HasMaxLength(180);

            builder.Property(x => x.FromWarehouseId)
                .IsRequired(true)
                .HasColumnType("BIGINT");
            
            builder.Property(x => x.ToWarehouseId)
                .IsRequired(true)
                .HasColumnType("BIGINT");

            builder.Property(x => x.TransferStatus)
                .IsRequired(true)
                .HasColumnType("VARCHAR");

            builder.Property(x => x.TransferNote)
                .HasColumnType("TEXT")
                .HasMaxLength(500);

            builder.Property(x => x.UserId)
                .IsRequired(true)
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);
        }
    }
}
