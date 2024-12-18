using InvenShopfy.Core.Models.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Notifications;

public class NotificationMapping : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notification");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.NotificationTitle)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(150);
        
        builder.Property(x => x.Urgency)
            .HasColumnType("BOOLEAN");
        
        builder.Property(x => x.Image)
            .IsRequired(false)
            .HasColumnType("TEXT");
      
        builder.Property(x => x.From)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(150);
        
        builder.Property(x => x.Href)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(150);
        
        builder.Property(x => x.CreateAt)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);
        
    }
}