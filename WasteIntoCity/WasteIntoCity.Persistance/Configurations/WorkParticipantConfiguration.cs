using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkParticipantConfiguration : IEntityTypeConfiguration<WorkParticipantEntity>
    {
        public const string TABLE_NAME = "work_participants";

        public void Configure(EntityTypeBuilder<WorkParticipantEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(w => w.WorksId).HasColumnName("works_id");

            builder.Property(w => w.ParticipantsId).HasColumnName("participants_id");

            builder.HasKey(w => new { w.WorksId, w.ParticipantsId });
        }
    }
}
