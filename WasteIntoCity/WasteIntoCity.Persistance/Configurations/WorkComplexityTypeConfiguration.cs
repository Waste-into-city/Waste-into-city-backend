using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkComplexityTypeConfiguration : IEntityTypeConfiguration<WorkComplexityTypeEntity>
    {
        public void Configure(EntityTypeBuilder<WorkComplexityTypeEntity> builder)
        {
            builder.ToTable("work_complexity_types");

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
                .HasForeignKey(wo => wo.WorkComplexitiesId);

            builder.HasMany(w => w.Works)
                .WithOne(wo => wo.WorkComplexityType)
                .HasForeignKey(wo => wo.WorkComplexityTypesId);
        }
    }
}
