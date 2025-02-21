using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkColleagueReportConfiguration : IEntityTypeConfiguration<WorkColleagueReportEntity>
    {
        public void Configure(EntityTypeBuilder<WorkColleagueReportEntity> builder)
        {
            builder.ToTable("work_colleague_reports");

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.FromParticipantId).IsRequired().HasColumnName("from_participant_id");

            builder.Property(w => w.AboutColleagueId).IsRequired().HasColumnName("about_colleague_id");

            builder.Property(w => w.WorksId).IsRequired().HasColumnName("works_id");

            builder.Property(w => w.WorkMarkTypesId).IsRequired().HasColumnName("work_mark_types_id");

            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.UserFromParticipant)
                .WithMany(u => u.WorkColleagueReportsFrom);

            builder.HasOne(w => w.UserAboutColleague)
                .WithMany(u => u.WorkColleagueReportsAbout);

            builder.HasOne(w => w.Work)
                .WithMany(u => u.WorkColleagueReports);

            builder.HasOne(w => w.WorkMarkType)
                .WithMany(u => u.WorkColleagueReports);
        }
    }
}
