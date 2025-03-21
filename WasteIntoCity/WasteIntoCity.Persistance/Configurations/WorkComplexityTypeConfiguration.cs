using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkComplexityTypeConfiguration : IEntityTypeConfiguration<WorkComplexityTypeEntity>
    {
        public const string TABLE_NAME = "work_complexity_types";

        public void Configure(EntityTypeBuilder<WorkComplexityTypeEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.Name).IsRequired().HasColumnName("name").HasMaxLength(WorkComplexityType.NAME_LENGTH_MAX);

            builder.Property(w => w.ParticipantsMin).IsRequired().HasColumnName("participants_min");

            builder.Property(w => w.ParticipantsMax).IsRequired().HasColumnName("participants_max");

            builder.Property(w => w.DurationHours).IsRequired().HasColumnName("duration_hours");

            builder.Property(w => w.MultiplierRanking).IsRequired().HasColumnName("multiplier_ranking");

            builder.Property(w => w.RadiusOnMap).IsRequired().HasColumnName("radius_on_map");

            builder.HasKey(w => w.Id);

            builder.HasIndex(w => w.Name).IsUnique();

            builder.HasMany(w => w.WorkApplications)
                .WithOne(wo => wo.WorkComplexityType)
                .HasForeignKey(wo => wo.WorkComplexityTypesId);

            builder.HasMany(w => w.Works)
                .WithOne(wo => wo.WorkComplexityType)
                .HasForeignKey(wo => wo.WorkComplexityTypesId);
        }
    }
}
