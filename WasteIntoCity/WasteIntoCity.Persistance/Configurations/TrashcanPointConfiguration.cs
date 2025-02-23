using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class TrashcanPointConfiguration : IEntityTypeConfiguration<TrashcanPointEntity>
    {
        public void Configure(EntityTypeBuilder<TrashcanPointEntity> builder)
        {
            builder.ToTable("trashcan_points");

            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.Lat).IsRequired().HasColumnName("lat").HasMaxLength(TrashcanPoint.LAT_LEGTH_MAX);

            builder.Property(t => t.Lng).IsRequired().HasColumnName("lng").HasMaxLength(TrashcanPoint.LNG_LEGTH_MAX);

            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.Trashcans)
                .WithOne(tr => tr.TrashcanPoint)
                .HasForeignKey(tr => tr.TrashcanPointsId);

            builder.HasMany(t => t.TrashcanPointReports)
                .WithOne(tp => tp.TrashcanPoint)
                .HasForeignKey(tp => tp.TrashcanPointsId);
        }
    }
}
