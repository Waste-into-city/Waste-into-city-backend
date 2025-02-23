using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkReportResultConfiguration : IEntityTypeConfiguration<WorkReportResultEntity>
    {
        public void Configure(EntityTypeBuilder<WorkReportResultEntity> builder)
        {
            builder.ToTable("work_report_results");

            builder.Property(w => w.Id);

            builder.Property(w => w.Title).IsRequired().HasColumnName("title").HasMaxLength(WorkReportResult.TITLE_LENGTH_MIN);

            builder.Property(w => w.Description).IsRequired().HasColumnName("description").HasMaxLength(WorkReportResult.DESCRIPTION_LENGTH_MAX);

            builder.Property(w => w.FromParticipantsId).IsRequired().HasColumnName("from_participant_id");

            builder.Property(w => w.WorkStatusTypesId).IsRequired().HasColumnName("work_statuses_id");

            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.FromParticipant)
                .WithMany(w => w.WorkReportResults);

            builder.HasOne(w => w.WorkStatusType)
                .WithMany(w => w.WorkReportResults);

            builder.HasMany(i => i.Images)
                .WithOne(w => w.WorkReportResult)
                .HasForeignKey(w => w.WorkReportResultsId);
        }
    }
}
