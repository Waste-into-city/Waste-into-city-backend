using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class AccessTokenConfiguration
    {
        private const int COLUMN_VALUE_LENGTH = 255;

        public void Configure(EntityTypeBuilder<AccessTokenEntity> builder)
        {
            //builder.ToTable("access_tokens");

            //builder.Property(a => a.UserId).HasColumnName("user_id");

            //builder.Property(a => a.Value).IsRequired().HasMaxLength(COLUMN_VALUE_LENGTH).HasColumnName("value");

            //builder.Property(a => a.ExpirationTimestamp).IsRequired().HasColumnName("expiration_timestamp");

            //builder.HasKey(a => a.UserId);

            //builder.HasOne(a => a.User)
            //    .WithOne(u => u.AccessToken)
            //    .HasForeignKey<AccessTokenEntity>(a => a.UserId);
        }
    }
}
