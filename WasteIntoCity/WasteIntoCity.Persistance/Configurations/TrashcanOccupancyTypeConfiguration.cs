using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class TrashcanOccupancyTypeConfiguration : IEntityTypeConfiguration<TrashcanOccupancyTypeEntity>
    {
        public const string TABLE_NAME = "trashcan_occupancy_types";

        public void Configure(EntityTypeBuilder<TrashcanOccupancyTypeEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.Name).IsRequired().HasColumnName("name").HasMaxLength(TrashcanOccupancyType.NAME_LENGTH_MAX);

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
