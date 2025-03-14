using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkReportResultConfiguration
    {
        public partial class WorkStatusTypeConfiguration : IEntityTypeConfiguration<WorkStatusTypeEntity>
        {
            public void Configure(EntityTypeBuilder<WorkStatusTypeEntity> builder)
            {
                builder.ToTable("work_status_types");

                builder.Property(w => w.Id).HasColumnName("id");

                builder.Property(w => w.Name).HasColumnName("name").HasMaxLength(WorkStatusType.NAME_LENGTH_MAX);

                builder.Property(w => w.MultiplierRanking).HasColumnName("multiplier_ranking");

                builder.HasKey(w => w.Id);

                builder.HasIndex(w => w.Name).IsUnique();

                builder.HasMany(w => w.Works)
                    .WithOne(wo => wo.WorkStatusType)
                    .HasForeignKey(wo => wo.WorkStatusTypesId);

                builder.HasMany(w => w.WorkReportResults)
                    .WithOne(wo => wo.WorkStatusType)
                    .HasForeignKey(wo => wo.WorkStatusTypesId);
            }
        }
    }
}
