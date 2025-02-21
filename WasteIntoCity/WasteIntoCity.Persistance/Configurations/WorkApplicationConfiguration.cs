using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkApplicationConfiguration : IEntityTypeConfiguration<WorkApplicationEntity>
    {
        public void Configure(EntityTypeBuilder<WorkApplicationEntity> builder)
        {
            builder.ToTable("work_applications");

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.Title).IsRequired().HasColumnName("title");

            builder.Property(w => w.Description).IsRequired().HasColumnName("description");

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
