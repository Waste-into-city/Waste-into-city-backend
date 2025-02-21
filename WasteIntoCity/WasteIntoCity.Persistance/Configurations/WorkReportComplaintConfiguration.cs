using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkReportComplaintConfiguration : IEntityTypeConfiguration<WorkReportComplaintEntity>
    {
        public void Configure(EntityTypeBuilder<WorkReportComplaintEntity> builder)
        {
            builder.ToTable("work_report_complaints");

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.Title).IsRequired().HasColumnName("title");

            builder.Property(w => w.Description).IsRequired().HasColumnName("description");

            builder.Property(w => w.WorksId).IsRequired().HasColumnName("works_id");

            builder.Property(w => w.FromUsersId).IsRequired().HasColumnName("from_users_id");

            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.Work)
                .WithMany(w => w.WorkReportComplaints);

            builder.HasMany(i => i.Images)
                .WithOne(w => w.WorkReportComplaint)
                .HasForeignKey(w => w.WorkReportComplaintsId);

            builder.HasOne(w => w.FromUser)
                .WithMany(w => w.WorkReportComplaints);
        }
    }
}
