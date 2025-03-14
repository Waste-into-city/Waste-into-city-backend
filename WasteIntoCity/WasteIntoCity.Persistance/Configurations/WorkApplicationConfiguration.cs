using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkApplicationConfiguration : IEntityTypeConfiguration<WorkApplicationEntity>
    {
        public void Configure(EntityTypeBuilder<WorkApplicationEntity> builder)
        {
            builder.ToTable("work_applications");

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.Title).IsRequired().HasColumnName("title").HasMaxLength(WorkApplication.TITLE_LENGTH_MAX);

            builder.Property(w => w.Description).IsRequired().HasColumnName("description").HasMaxLength(WorkApplication.DESCRIPTION_LENGTH_MAX);

            builder.Property(w => w.WorkComplexitiesId).IsRequired().HasColumnName("work_complexities_id");

            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.WorkComplexityType)
                .WithMany(wo => wo.WorkApplications);

            builder.HasMany(w => w.Images)
                .WithOne(i => i.WorkApplication)
                .HasForeignKey(i => i.WorkApplicationsId);
        }
    }
}
