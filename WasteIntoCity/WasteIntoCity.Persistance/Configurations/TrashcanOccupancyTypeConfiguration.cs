using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class TrashcanOccupancyTypeConfiguration : IEntityTypeConfiguration<TrashcanOccupancyTypeEntity>
    {
        public void Configure(EntityTypeBuilder<TrashcanOccupancyTypeEntity> builder)
        {
            builder.ToTable("trashcan_occupancy_types");

            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.Name).IsRequired().HasColumnName("name");

            builder.Property(t => t.Value).IsRequired().HasColumnName("value");

            builder.HasKey(t => t.Id);

            builder.HasIndex(t => t.Name).IsUnique();

            builder.HasMany(t => t.Trashcans)
                .WithOne(tr => tr.AverageTrashcanOccupancyType)
                .HasForeignKey(tr => tr.AverageTrashcanOccupancyTypeId);

            builder.HasMany(t => t.TrashcanPointReportEachMarkList)
                .WithOne(tp => tp.TrashcanOccupancyType)
                .HasForeignKey(tp => tp.TrashcanOccupancyTypesId);
        }
    }
}
