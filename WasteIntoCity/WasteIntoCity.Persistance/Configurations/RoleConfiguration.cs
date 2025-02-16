using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(r => r.Id).HasName("id");

            builder.Property(r => r.Name).IsRequired().HasColumnName("name");

            builder.HasIndex(r => r.Name).IsUnique();

            builder.HasMany(r => r.Users)
                .WithMany(u => u.Roles)
                .UsingEntity<UserAccordingRoleEntity>(
                    r => r.HasOne<UserEntity>().WithMany().HasForeignKey("FK_users_according_role_users"),
                    l => l.HasOne<RoleEntity>().WithMany().HasForeignKey("FK_users_according_role_roles")
                );

            IEnumerable<RoleEntity> roles = Enum.GetValues<RoleEnum>().Select(r => new RoleEntity
            {
                Id = (int)r,
                Name = r.ToString()
            });

            builder.HasData(roles);
        }
    }
}
