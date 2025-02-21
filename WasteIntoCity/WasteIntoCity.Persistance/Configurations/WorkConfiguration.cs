using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkConfiguration : IEntityTypeConfiguration<WorkEntity>
    {
        public void Configure(EntityTypeBuilder<WorkEntity> builder)
        {
            builder.ToTable("works");

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.Title).IsRequired().HasColumnName("title");

            builder.Property(w => w.Description).IsRequired().HasColumnName("description");

            builder.Property(w => w.StartedDatetime).IsRequired().HasColumnName("start_datetime");

            builder.Property(w => w.FinishDatetime).IsRequired().HasColumnName("finish_datetime");

            builder.Property(w => w.WorkComplexityTypesId).IsRequired().HasColumnName("work_complexity_id");

            builder.Property(w => w.WorkStatusTypesId).IsRequired().HasColumnName("work_statuses_id");

            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.WorkComplexityType)
                .WithMany(wo => wo.Works);

            builder.HasOne(w => w.WorkStatusType)
                .WithMany(wo => wo.Works);

            builder.HasMany(w => w.WorkReportComplaints)
                .WithOne(wo => wo.Work)
                .HasForeignKey(wo => wo.WorksId);

            builder.HasMany(w => w.WorkColleagueReports)
                .WithOne(wo => wo.Work)
                .HasForeignKey(wo => wo.WorksId);

            builder.HasMany(w => w.Users)
                .WithMany(u => u.Works)
                .UsingEntity<WorkParticipantEntity>(
                    w => w.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.ParticipantsId),
                    u => u.HasOne<WorkEntity>().WithMany().HasForeignKey(e => e.WorksId)
                );
        }
    }
}
