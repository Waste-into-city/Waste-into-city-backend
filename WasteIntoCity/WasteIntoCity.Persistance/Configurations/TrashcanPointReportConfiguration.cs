using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class TrashcanPointReportConfiguration : IEntityTypeConfiguration<TrashcanPointReportEntity>
    {
        public const string TABLE_NAME = "trashcan_point_reports";

        public void Configure(EntityTypeBuilder<TrashcanPointReportEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.IsReviewed).IsRequired().HasColumnName("is_required");

            builder.Property(t => t.UsersId).IsRequired().HasColumnName("users_id");

            builder.Property(t => t.CoordinatesId).IsRequired().HasColumnName("coordinates_id");

            builder.Property(t => t.SubmissionTime).IsRequired().HasColumnName("submission_time");

            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.User)
                .WithMany(u => u.TrashcanPointReports);

            builder.HasOne(t => t.Coordinates)
                .WithMany(tr => tr.TrashcanPointReports);

            builder.HasMany(t => t.TrashcanPointReportEachMarkList)
                .WithOne(tr => tr.TrashcanPointReport)
                .HasForeignKey(tr => tr.TrashcanPointReportsId);
        }
    }
}
