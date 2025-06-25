using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkTrashTypeConfiguration : IEntityTypeConfiguration<WorkTrashTypeEntity>
    {
        public const string TABLE_NAME = "works_trash_types";

        public void Configure(EntityTypeBuilder<WorkTrashTypeEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(w => w.WorksId).HasColumnName("works_id");

            builder.Property(w => w.TrashTypesId).HasColumnName("trash_types_id");

            builder.HasKey(w => new { w.WorksId, w.TrashTypesId });
        }
    }
}
