using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkApplicationConfiguration : IEntityTypeConfiguration<WorkApplicationEntity>
    {
        public const string TABLE_NAME = "work_applications";

        public void Configure(EntityTypeBuilder<WorkApplicationEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.Title).IsRequired().HasColumnName("title").HasMaxLength(WorkApplication.TITLE_LENGTH_MAX);

            builder.Property(w => w.Description).IsRequired().HasColumnName("description").HasMaxLength(WorkApplication.DESCRIPTION_LENGTH_MAX);

            builder.Property(w => w.StartedDatetime).IsRequired().HasColumnName("started_datetime");

            builder.Property(w => w.WorkComplexityTypesId).IsRequired().HasColumnName("work_complexity_types_id");

            builder.Property(w => w.CoordinatesId).IsRequired().HasColumnName("coordinates_id");

            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.WorkComplexityType)
                .WithMany(wo => wo.WorkApplications);

            builder.HasMany(w => w.Images)
                .WithOne(i => i.WorkApplication)
                .HasForeignKey(i => i.WorkApplicationsId);

            builder.HasOne(w => w.Coordinates)
                .WithMany(c => c.WorkApplications);

            builder.HasOne(w => w.FromUser)
                .WithMany(u => u.WorkApplications);
        }
    }
}
