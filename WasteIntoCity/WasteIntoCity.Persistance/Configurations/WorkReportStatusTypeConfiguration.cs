using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkReportStatusTypeConfiguration : IEntityTypeConfiguration<WorkReportStatusTypeEntity>
    {
        public const string TABLE_NAME = "work_report_status_types";

        public void Configure(EntityTypeBuilder<WorkReportStatusTypeEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.Name).IsRequired().HasColumnName("name").HasMaxLength(WorkReportStatusType.NAME_LENGTH_MAX);

            builder.HasKey(w => w.Id);

            builder.HasMany(w => w.WorkReportComplaints)
                .WithOne(wo => wo.WorkReportComplaintType)
                .HasForeignKey(w => w.WorkReportStatusTypesId);

            builder.HasMany(w => w.WorkApplications)
                .WithOne(wo => wo.WorkReportStatusType)
                .HasForeignKey(w => w.WorkReportStatusTypesId);
        }
    }
}
