using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity = Trench.Notification.Domain.Aggregates.Notification.Entities;

namespace Trench.Notification.Persistence.Postgres.Mappings.Notification;

internal sealed class NotificationMapping 
    : IEntityTypeConfiguration<Entity.Notification>
{
    public void Configure(EntityTypeBuilder<Entity.Notification> builder)
    {
        builder.ToTable("notification");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);

        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.RecipientUserId)
            .IsRequired();

        builder.Property(x => x.TriggeringUserId)
            .IsRequired();

        builder.Property(x => x.IsRead)
            .IsRequired();
    }
}