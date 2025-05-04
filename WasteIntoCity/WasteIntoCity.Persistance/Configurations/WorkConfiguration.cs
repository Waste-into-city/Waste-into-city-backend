using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkConfiguration : IEntityTypeConfiguration<WorkEntity>
    {
        public const string TABLE_NAME = "works";

        public void Configure(EntityTypeBuilder<WorkEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.Title).IsRequired().HasColumnName("title").HasMaxLength(Work.TITLE_LENGTH_MAX);

            builder.Property(w => w.Description).IsRequired().HasColumnName("description").HasMaxLength(Work.DESCRIPTION_LENGTH_MAX);

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

            builder.HasOne(w => w.Coordinates)
                .WithMany(c => c.Works);

            builder.HasMany(w => w.Users)
                .WithMany(u => u.Works)
                .UsingEntity<WorkParticipantEntity>(
                    w => w.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.ParticipantsId),
                    u => u.HasOne<WorkEntity>().WithMany().HasForeignKey(e => e.WorksId)
                );

            builder.HasMany(w => w.TrashTypes)
                .WithMany(t => t.Works)
                .UsingEntity<WorkTrashTypeEntity>(
                    w => w.HasOne<TrashTypeEntity>().WithMany().HasForeignKey(e => e.TrashTypesId),
                    t => t.HasOne<WorkEntity>().WithMany().HasForeignKey(e => e.WorksId)
                );

            builder.HasMany(w => w.Images)
                .WithOne(i => i.Work)
                .HasForeignKey(i => i.WorksId);

            builder.HasOne(w => w.WorkReportResult)
                .WithOne(wo => wo.Work);
        }
    }
}
