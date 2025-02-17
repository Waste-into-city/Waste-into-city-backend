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

            builder.Property(w => w.Name).IsRequired().HasColumnName("name");

            builder.Property(w => w.AdditionRanking).IsRequired().HasColumnName("addition_ranking");

            builder.HasKey(w => w.Id);

            builder.HasIndex(w => w.Name).IsUnique();

            builder.HasMany(w => w.WorkColleagueReports)
                .WithOne(w => w.WorkMarkType)
                .HasForeignKey("FK_work_colleague_reports_work_mark_types");
        }
    }
}
