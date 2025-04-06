using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkStatusTypeConfiguration : IEntityTypeConfiguration<WorkStatusTypeEntity>
    {
        public const string TABLE_NAME = "work_status_types";

        public void Configure(EntityTypeBuilder<WorkStatusTypeEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.Name).HasColumnName("name").HasMaxLength(WorkStatusType.NAME_LENGTH_MAX);

            builder.Property(w => w.AddingRanking).HasColumnName("multiplier_ranking");

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
