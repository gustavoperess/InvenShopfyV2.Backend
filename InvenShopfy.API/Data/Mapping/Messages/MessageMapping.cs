using InvenShopfy.Core.Models.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Messages;

public class MessageMapping : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Message");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasColumnType("VARCHAR")
            .HasMaxLength(150);
        
        builder.Property(x => x.Subtitle)
            .HasColumnType("VARCHAR")
            .HasMaxLength(150);
        
        builder.Property(x => x.ToUserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
        builder.Property(x => x.Description)
            .HasColumnType("TEXT")
            .HasMaxLength(500);
        
        builder.Property(x => x.Time)
            .HasColumnType("date");
        
        builder.Property(x => x.IsImportant)
            .HasColumnType("BOOLEAN");
        
        builder.Property(x => x.IsDeleted)
            .HasColumnType("BOOLEAN");
        
        builder.Property(x => x.IsSent)
            .HasColumnType("BOOLEAN");
        
        builder.Property(x => x.IsReceived)
            .HasColumnType("BOOLEAN");
        
        builder.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
        
    }
}