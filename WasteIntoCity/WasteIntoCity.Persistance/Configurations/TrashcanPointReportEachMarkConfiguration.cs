using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class TrashcanPointReportEachMarkConfiguration : IEntityTypeConfiguration<TrashcanPointReportEachMarkEntity>
    {
        public void Configure(EntityTypeBuilder<TrashcanPointReportEachMarkEntity> builder)
        {
            builder.ToTable("trashcan_point_report_each_mark");

            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.TrashcanPointReportsId).IsRequired().HasColumnName("trashcan_point_reports_id");

            builder.Property(t => t.TrashcansId).IsRequired().HasColumnName("trashcans_id");

            builder.Property(t => t.TrashcanOccupancyTypesId).IsRequired().HasColumnName("trashcan_occupancy_types_id");

            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Trashcan)
                .WithOne(tr => tr.TrashcanPointReportEachMark);

            builder.HasOne(t => t.TrashcanPointReport)
                .WithMany(tr => tr.TrashcanPointReportEachMarkList);

            builder.HasOne(t => t.TrashcanOccupancyType)
                .WithMany(tr => tr.TrashcanPointReportEachMarkList);
        }
    }
}
