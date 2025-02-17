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

            builder.Property(t => t.Lat).IsRequired().HasColumnName("lat");

            builder.Property(t => t.Lng).IsRequired().HasColumnName("lng");

            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.Trashcans)
                .WithOne(tr => tr.TrashcanPoint).HasForeignKey("FK_trashcans_trashcan_point");

            builder.HasMany(t => t.TrashcanPointReports)
                .WithOne(tp => tp.TrashcanPoint).HasForeignKey("FK_trashcan_point_reports_trashcan_points");
        }
    }
}
