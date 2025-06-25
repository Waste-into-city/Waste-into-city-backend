using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class NotificationConfiguration : IEntityTypeConfiguration<NotificationEntity>
    {
        public const string TABLE_NAME = "notifications";

        public void Configure(EntityTypeBuilder<NotificationEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(n => n.Id).HasColumnName("id");

            builder.Property(n => n.Title).IsRequired().HasColumnName("title").HasMaxLength(Notification.TITLE_LENGTH_MAX);

            builder.Property(n => n.Description).IsRequired().HasColumnName("description").HasMaxLength(Notification.DESCRIPTION_LENGTH_MAX);

            builder.Property(n => n.FromUsersId).IsRequired().HasColumnName("from_users_id");

            builder.Property(n => n.ToUsersId).IsRequired().HasColumnName("to_users_id");

            builder.HasKey(n => n.Id);

            builder.HasOne(n => n.FromUser)
                .WithMany(u => u.NotificationsFrom);

            builder.HasOne(n => n.ToUser)
                .WithMany(u => u.NotificationsTo);
        }
    }
}
