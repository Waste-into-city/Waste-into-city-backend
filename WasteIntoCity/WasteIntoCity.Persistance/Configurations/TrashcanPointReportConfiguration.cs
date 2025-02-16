using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class TrashcanPointReportConfiguration : IEntityTypeConfiguration<TrashcanPointReportEntity>
    {
        public void Configure(EntityTypeBuilder<TrashcanPointReportEntity> builder)
        {
            builder.HasKey(t => t.Id).HasName("id");

            builder.Property(t => t.IsReviewed).IsRequired().HasColumnName("is_required");

            builder.Property(t => t.UsersId).IsRequired().HasColumnName("users_id");

            builder.Property(t => t.TrashcanPointsId).IsRequired().HasColumnName("trashcan_points_id");

            builder.Property(t => t.SubmissionTime).IsRequired().HasColumnName("submission_time");

            builder.HasOne(t => t.User)
                .WithMany(u => u.TrashcanPointReports);

            builder.HasOne(t => t.TrashcanPoint)
                .WithMany(tr => tr.TrashcanPointReports);

            builder.HasMany(t => t.TrashcanPointReportEachMarkList)
                .WithOne(tr => tr.TrashcanPointReport).HasForeignKey("FK_trashcan_point_report_each_mark_trashcan_point_reports");
        }
    }
}
