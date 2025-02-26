using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class ImageConfiguration : IEntityTypeConfiguration<ImageEntity>
    {
        public void Configure(EntityTypeBuilder<ImageEntity> builder)
        {
            builder.ToTable("images");

            builder.Property(i => i.Id).HasColumnName("id");

            builder.Property(i => i.Name).IsRequired().HasColumnName("name").HasMaxLength(Image.NAME_LENGTH_MAX);

            builder.Property(i => i.WorkApplicationsId).HasColumnName("work_applications_id");

            builder.Property(i => i.WorkReportComplaintsId).HasColumnName("workReport_complaints_id");

            builder.Property(i => i.WorkReportResultsId).HasColumnName("work_report_results_id");

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.WorkApplication)
                .WithMany(wa => wa.Images);

            builder.HasOne(i => i.WorkReportComplaint)
                .WithMany(wa => wa.Images);

            builder.HasOne(i => i.WorkReportResult)
                .WithMany(wa => wa.Images);
        }
    }
}
