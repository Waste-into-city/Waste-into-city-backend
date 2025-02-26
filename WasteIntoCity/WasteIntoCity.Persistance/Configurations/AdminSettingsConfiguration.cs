using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class AdminSettingsConfiguration : IEntityTypeConfiguration<AdminSettingsEntity>
    {
        public void Configure(EntityTypeBuilder<AdminSettingsEntity> builder)
        {
            builder.ToTable("admin_settings");

            builder.Property(a => a.Id).HasColumnName("id");

            builder.Property(a => a.TrueComplaintToAdditionRanking).IsRequired().HasColumnName("true_complaint_to_addition_ranking");

            builder.Property(a => a.FalseComplaintFromAdditionRanking).IsRequired().HasColumnName("false_complaint_from_addition_ranking");

            builder.Property(a => a.TrueComplaintFromAdditionRanking).IsRequired().HasColumnName("true_complaint_from_addition_ranking");

            builder.Property(a => a.AcceptableDifferenceReportTrashcanOccupancy).IsRequired().HasColumnName("acceptable_difference_report_trashcan_occupancy");

            builder.Property(a => a.FalseReportTrashcansOccupancyAdditionRanking).IsRequired().HasColumnName("false_report_trashcans_occupancy_addition_ranking");

            builder.HasKey(a => a.Id);
        }
    }

    public partial class AccessTokenConfiguration : IEntityTypeConfiguration<AccessTokenEntity>
    {
        private const int COLUMN_VALUE_LENGTH = 255;

        public void Configure(EntityTypeBuilder<AccessTokenEntity> builder)
        {
            builder.ToTable("access_tokens");

            builder.Property(a => a.UserId).HasColumnName("user_id");

            builder.Property(a => a.Value).IsRequired().HasMaxLength(COLUMN_VALUE_LENGTH).HasColumnName("value");

            builder.Property(a => a.ExpirationTimestamp).IsRequired().HasColumnName("expiration_timestamp");

            builder.HasKey(a => a.UserId);

            builder.HasOne(a => a.User)
                .WithOne(u => u.AccessToken)
                .HasForeignKey<AccessTokenEntity>(a => a.UserId);
        }
    }

    public partial class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        private const int COLUMN_VALUE_LENGTH = 255;

        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            builder.ToTable("refresh_tokens");

            builder.Property(t => t.UserId).HasColumnName("user_id");

            builder.Property(t => t.Value).IsRequired().HasMaxLength(COLUMN_VALUE_LENGTH).HasColumnName("value");

            builder.Property(t => t.ExpirationTimestamp).IsRequired().HasColumnName("expiration_timestamp");

            builder.HasKey(t => t.UserId);

            builder.HasOne(t => t.User)
                .WithOne(u => u.RefreshToken)
                .HasForeignKey<RefreshTokenEntity>(t => t.UserId);
        }
    }
}
