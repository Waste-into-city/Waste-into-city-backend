using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        private const int COLUMN_VALUE_LENGTH = 255;

        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            builder.ToTable("refresh_tokens");

            builder.Property(t => t.UserId).HasColumnName("user_id");

            builder.Property(t => t.Value).IsRequired().HasMaxLength(COLUMN_VALUE_LENGTH).HasColumnName("value");

            builder.Property(t => t.ExpirationTimestamp).IsRequired().HasColumnName("expiration_timestamp");

            builder.HasKey(t => t.UserId);

            builder.HasOne(t => t.User)
                .WithOne(u => u.RefreshToken)
                .HasForeignKey<RefreshTokenEntity>(t => t.UserId);
        }
    }
}
