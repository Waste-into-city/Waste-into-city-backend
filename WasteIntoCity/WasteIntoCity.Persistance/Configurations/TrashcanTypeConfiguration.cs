using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class TrashcanTypeConfiguration : IEntityTypeConfiguration<TrashcanTypeEntity>
    {
        public void Configure(EntityTypeBuilder<TrashcanTypeEntity> builder)
        {
            builder.ToTable("trashcan_types");

            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.Name).IsRequired().HasColumnName("name");

            builder.HasKey(t => t.Id);

            builder.HasIndex(t => t.Name).IsUnique();

            builder.HasMany(t => t.Trashcans)
                .WithOne(tr => tr.TrashcanType)
                .HasForeignKey(tr => tr.TrashcanTypesId);
        }
    }
}
