using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        private const int COLUMN_JWT_ID_LENGTH = 255;

        public const string TABLE_NAME = "refresh_tokens";

        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(t => t.Value).HasColumnName("value");

            builder.Property(t => t.JwtId).IsRequired().HasMaxLength(COLUMN_JWT_ID_LENGTH).HasColumnName("jwt_id");

            builder.Property(t => t.CreationTimestamp).IsRequired().HasColumnName("creation_timestamp");

            builder.Property(t => t.ExpirationTimestamp).IsRequired().HasColumnName("expiration_timestamp");

            builder.Property(t => t.Used).HasColumnName("used").IsRequired();

            builder.Property(t => t.Invalidated).HasColumnName("invalidated").IsRequired();

            builder.Property(t => t.UserId).HasColumnName("user_id").IsRequired();

            builder.HasKey(t => t.Value);

            builder.HasOne(t => t.User)
                .WithMany(u => u.RefreshTokens);
        }
    }
}
