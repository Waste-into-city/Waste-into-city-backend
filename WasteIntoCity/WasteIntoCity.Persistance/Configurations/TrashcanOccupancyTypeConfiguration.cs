using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class TrashcanOccupancyTypeConfiguration : IEntityTypeConfiguration<TrashcanOccupancyTypeEntity>
    {
        public void Configure(EntityTypeBuilder<TrashcanOccupancyTypeEntity> builder)
        {
            builder.HasKey(t => t.Id).HasName("id");

            builder.Property(t => t.Name).IsRequired().HasColumnName("name");

            builder.Property(t => t.Value).IsRequired().HasColumnName("value");

            builder.HasIndex(t => t.Name).IsUnique();

            builder.HasMany(t => t.Trashcans)
                .WithOne(tr => tr.AverageTrashcanOccupancyType).HasForeignKey("FK_trashcans_trashcan_occupancy_types");

            builder.HasMany(t => t.TrashcanPointReportEachMarkList)
                .WithOne(tp => tp.TrashcanOccupancyType).HasForeignKey("FK_trashcan_point_report_each_mark_trashcan_occupancy_types");
        }
    }
}
