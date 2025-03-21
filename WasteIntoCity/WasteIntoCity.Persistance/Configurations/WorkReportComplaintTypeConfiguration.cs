using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkReportComplaintTypeConfiguration : IEntityTypeConfiguration<WorkReportComplaintTypeEntity>
    {
        public void Configure(EntityTypeBuilder<WorkReportComplaintTypeEntity> builder)
        {
            builder.ToTable("work_report_complaint_types");

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.Name).IsRequired().HasColumnName("name").HasMaxLength(WorkReportComplaintType.NAME_LENGTH_MAX);

            builder.HasKey(w => w.Id);

            builder.HasMany(w => w.WorkReportComplaints)
                .WithOne(wo => wo.WorkReportComplaintType)
                .HasForeignKey(w => w.WorkReportComplaintTypesId);
        }
    }
}
