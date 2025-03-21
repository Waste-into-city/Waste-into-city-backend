using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class TrashcanTypeConfiguration : IEntityTypeConfiguration<TrashcanTypeEntity>
    {
        public const string TABLE_NAME = "trashcan_types";

        public void Configure(EntityTypeBuilder<TrashcanTypeEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.Name).IsRequired().HasColumnName("name").HasMaxLength(TrashcanType.NAME_LENGTH_MAX);

            builder.HasKey(t => t.Id);

            builder.HasIndex(t => t.Name).IsUnique();

            builder.HasMany(t => t.Trashcans)
                .WithOne(tr => tr.TrashcanType)
                .HasForeignKey(tr => tr.TrashcanTypesId);
        }
    }
}
