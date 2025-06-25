using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkApplicationTrashTypeConfiguration : IEntityTypeConfiguration<WorkApplicationTrashTypeEntity>
    {
        public const string TABLE_NAME = "work_applications_trash_types";

        public void Configure(EntityTypeBuilder<WorkApplicationTrashTypeEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(w => w.WorksApplicationsId).HasColumnName("work_applications");

            builder.Property(w => w.TrashTypesId).HasColumnName("trash_types_id");

            builder.HasKey(w => new { w.WorksApplicationsId, w.TrashTypesId });
        }
    }
}
