using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class WorkParticipantConfiguration : IEntityTypeConfiguration<WorkParticipantEntity>
    {
        public void Configure(EntityTypeBuilder<WorkParticipantEntity> builder)
        {
            builder.ToTable("work_participants");

            builder.Property(w => w.WorksId).HasColumnName("works");

            builder.Property(w => w.ParticipantsId).HasColumnName("participants_id");

            builder.HasKey(w => new { w.WorksId, w.ParticipantsId });
        }
    }
}
