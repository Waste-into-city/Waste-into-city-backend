using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class CoordinatesConfiguration : IEntityTypeConfiguration<CoordinatesEntity>
    {
        public const string TABLE_NAME = "coordinates";

        public void Configure(EntityTypeBuilder<CoordinatesEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(c => c.Id).HasColumnName("id");

            builder.Property(c => c.Lat).IsRequired().HasColumnName("lat");

            builder.Property(c => c.Lng).IsRequired().HasColumnName("lng");

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
