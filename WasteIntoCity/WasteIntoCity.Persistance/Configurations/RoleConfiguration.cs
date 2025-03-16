using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        private const int NAME_PROPERTY_MAX_LENGTH = 45;

        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("roles");

            builder.Property(r => r.Id).HasColumnName("id");

            builder.Property(r => r.Name).IsRequired().HasColumnName("name").HasMaxLength(NAME_PROPERTY_MAX_LENGTH);

            builder.HasKey(r => r.Id);

            builder.HasIndex(r => r.Name).IsUnique();

            builder.HasMany(r => r.Users)
                .WithMany(u => u.Roles)
                .UsingEntity<UserAccordingRoleEntity>(
                    u => u.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.UsersId),
                    r => r.HasOne<RoleEntity>().WithMany().HasForeignKey(e => e.RolesId)
                );

            IEnumerable<RoleEntity> roles = Enum.GetValues<RoleType>().Select(r => new RoleEntity
            {
                Id = (int)r,
                Name = r.ToString()
            });

            builder.HasData(roles);
        }
    }
}
