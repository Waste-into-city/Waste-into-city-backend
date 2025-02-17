using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class TrashcanConfiguration : IEntityTypeConfiguration<TrashcanEntity>
    {
        public void Configure(EntityTypeBuilder<TrashcanEntity> builder)
        {
            builder.ToTable("trashcans");

            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.Volume).IsRequired().HasColumnName("volume");

            builder.Property(t => t.TrashcanTypesId).IsRequired().HasColumnName("trashcan_types_id");

            builder.Property(t => t.TrashcanPointsId).IsRequired().HasColumnName("trashcan_points_id");

            builder.Property(t => t.AverageTrashcanOccupancyTypeId).HasColumnType("average_trashcan_occupancy_type_id");

            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.TrashcanPoint)
                .WithMany(tp => tp.Trashcans);

            builder.HasOne(t => t.TrashcanType)
                .WithMany(tt => tt.Trashcans);

            builder.HasOne(t => t.AverageTrashcanOccupancyType)
                .WithMany(to => to.Trashcans);

            builder.HasOne(t => t.TrashcanPointReportEachMark)
                .WithOne(tpreml => tpreml.Trashcan).HasForeignKey("FK_trashcan_point_report_each_mark_trashcans");
        }
    }
}
