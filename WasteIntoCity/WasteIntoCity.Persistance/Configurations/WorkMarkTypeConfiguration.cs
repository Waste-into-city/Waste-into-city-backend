using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkMarkTypeConfiguration : IEntityTypeConfiguration<WorkMarkTypeEntity>
    {
        public void Configure(EntityTypeBuilder<WorkMarkTypeEntity> builder)
        {
            builder.ToTable("work_mark_types");

            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.Name).IsRequired().HasColumnName("name").HasMaxLength(WorkMarkType.NAME_LENGTH_MAX);

            builder.Property(w => w.AdditionRanking).IsRequired().HasColumnName("addition_ranking");

            builder.HasKey(w => w.Id);

            builder.HasIndex(w => w.Name).IsUnique();

            builder.HasMany(w => w.WorkColleagueReports)
                .WithOne(wo => wo.WorkMarkType)
                .HasForeignKey(wo => wo.WorkMarkTypesId);
        }
    }
}
