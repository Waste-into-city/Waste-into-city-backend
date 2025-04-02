using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkReportComplaintConfiguration : IEntityTypeConfiguration<WorkReportComplaintEntity>
    {
        public const string TABLE_NAME = "work_report_complaints";

        public void Configure(EntityTypeBuilder<WorkReportComplaintEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.Title).IsRequired().HasColumnName("title").HasMaxLength(WorkReportComplaint.TITLE_LENGTH_MAX);

            builder.Property(w => w.Description).IsRequired().HasColumnName("description").HasMaxLength(WorkReportComplaint.DESCRIPTION_LENGTH_MAX);

            builder.Property(w => w.StartedDatime).IsRequired().HasColumnName("started_datetime");

            builder.Property(w => w.WorksId).IsRequired().HasColumnName("works_id");

            builder.Property(w => w.FromUsersId).IsRequired().HasColumnName("from_users_id");

            builder.Property(w => w.WorkReportStatusTypesId).IsRequired().HasColumnName("work_report_complaint_status_types_id");

            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.Work)
                .WithMany(w => w.WorkReportComplaints);

            builder.HasMany(i => i.Images)
                .WithOne(w => w.WorkReportComplaint)
                .HasForeignKey(w => w.WorkReportComplaintsId);

            builder.HasOne(w => w.FromUser)
                .WithMany(w => w.WorkReportComplaints);

            builder.HasOne(w => w.WorkReportComplaintType)
                .WithMany(wo => wo.WorkReportComplaints);
        }
    }
}
