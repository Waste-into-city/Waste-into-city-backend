using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class NotificationConfiguration : IEntityTypeConfiguration<NotificationEntity>
    {
        public void Configure(EntityTypeBuilder<NotificationEntity> builder)
        {
            builder.HasKey(n => n.Id).HasName("id");

            builder.Property(n => n.Title).IsRequired().HasColumnName("title");

            builder.Property(n => n.Description).IsRequired().HasColumnName("description");

            builder.Property(n => n.FromUsersId).IsRequired().HasColumnName("from_users_id");

            builder.Property(n => n.ToUsersId).IsRequired().HasColumnName("to_users_id");

            builder.HasOne(n => n.FromUser)
                .WithMany(u => u.NotificationsFrom);

            builder.HasOne(n => n.ToUser)
                .WithMany(u => u.NotificationsTo);
        }
    }
}
