using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class TrashTypeConfiguration : IEntityTypeConfiguration<TrashTypeEntity>
    {
        public const string TABLE_NAME = "trash_types";

        public void Configure(EntityTypeBuilder<TrashTypeEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(u => u.Id).HasColumnName("id");

            builder.Property(u => u.Name).IsRequired().HasColumnName("name").HasMaxLength(TrashType.NAME_LENGTH_MAX);

            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Name).IsUnique();
        }
    }
}
