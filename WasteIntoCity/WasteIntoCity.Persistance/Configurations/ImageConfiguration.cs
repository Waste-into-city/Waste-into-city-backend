using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class ImageConfiguration : IEntityTypeConfiguration<ImageEntity>
    {
        public const string TABLE_NAME = "images";

        public void Configure(EntityTypeBuilder<ImageEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(i => i.Id).HasColumnName("id");

            builder.Property(i => i.Name).IsRequired().HasColumnName("name").HasMaxLength(ImageName.VALUE_LENGTH_MAX);

            builder.Property(i => i.UploadedTime).IsRequired().HasColumnName("uploaded_time");

            builder.Property(i => i.WorkApplicationsId).HasColumnName("work_applications_id");

            builder.Property(i => i.WorkReportComplaintsId).HasColumnName("workReport_complaints_id");

            builder.Property(i => i.WorkReportResultsId).HasColumnName("work_report_results_id");

            builder.HasKey(i => i.Id);

            builder.HasIndex(i => i.Name).IsUnique();

            builder.HasOne(i => i.WorkApplication)
                .WithMany(wa => wa.Images);

            builder.HasOne(i => i.WorkReportComplaint)
                .WithMany(wa => wa.Images);

            builder.HasOne(i => i.WorkReportResult)
                .WithMany(wa => wa.Images);
        }
    }
}
