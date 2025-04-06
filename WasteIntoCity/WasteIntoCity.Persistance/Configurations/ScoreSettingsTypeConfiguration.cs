using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class ScoreSettingsTypeConfiguration : IEntityTypeConfiguration<ScoreSettingsTypeEntity>
    {
        public const string TABLE_NAME = "score_settings_types";

        public void Configure(EntityTypeBuilder<ScoreSettingsTypeEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(s => s.Id).HasColumnName("id");

            builder.Property(s => s.Name).IsRequired().HasMaxLength(ScoreSettingsType.NAME_LENGTH_MAX).HasColumnName("name");

            builder.Property(s => s.Value).IsRequired().HasColumnName("value");

            builder.HasKey(s => s.Id);
        }
    }
}
