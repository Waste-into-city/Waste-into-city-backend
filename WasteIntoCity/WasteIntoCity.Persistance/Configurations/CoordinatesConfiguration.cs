using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class CoordinatesConfiguration : IEntityTypeConfiguration<CoordinatesEntity>
    {
        public void Configure(EntityTypeBuilder<CoordinatesEntity> builder)
        {
            builder.ToTable("coordinates");

            builder.Property(c => c.Id).HasColumnName("id");

            builder.Property(c => c.Lat).IsRequired().HasColumnName("lat").HasMaxLength(Coordinates.LAT_LEGTH_MAX);

            builder.Property(c => c.Lng).IsRequired().HasColumnName("lng").HasMaxLength(Coordinates.LNG_LEGTH_MAX);

            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Trashcans)
                .WithOne(tr => tr.Coordinates)
                .HasForeignKey(tr => tr.CoordinatesId);

            builder.HasMany(c => c.TrashcanPointReports)
                .WithOne(tp => tp.Coordinates)
                .HasForeignKey(tp => tp.CoordinatesId);

            builder.HasMany(c => c.Works)
                .WithOne(w => w.Coordinates)
                .HasForeignKey(w => w.CoordinatesId);

            builder.HasMany(c => c.WorkApplications)
                .WithOne(w => w.Coordinates)
                .HasForeignKey(w => w.CoordinatesId);
        }
    }
}
