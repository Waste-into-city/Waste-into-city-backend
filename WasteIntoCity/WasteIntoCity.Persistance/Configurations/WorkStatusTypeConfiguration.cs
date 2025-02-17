using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

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

                builder.Property(w => w.Name).HasColumnName("name");

                builder.Property(w => w.MultiplierRanking).HasColumnName("multiplier_ranking");

                builder.HasKey(w => w.Id);

                builder.HasIndex(w => w.Name).IsUnique();

                builder.HasMany(w => w.Works)
                    .WithOne(w => w.WorkStatusType)
                    .HasForeignKey("FK_works_work_status_types");

                builder.HasMany(w => w.WorkReportResults)
                    .WithOne(w => w.WorkStatusType)
                    .HasForeignKey("FK_work_report_results_work_status_types");
            }
        }
    }
}
