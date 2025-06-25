using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public const string TABLE_NAME = "roles";

        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(r => r.Id).HasColumnName("id");

            builder.Property(r => r.Name).IsRequired().HasColumnName("name").HasMaxLength(Role.NAME_LENGTH_MAX);

            builder.HasKey(r => r.Id);

            builder.HasIndex(r => r.Name).IsUnique();

            builder.HasMany(r => r.Users)
                .WithMany(u => u.Roles)
                .UsingEntity<UserAccordingRoleEntity>(
                    u => u.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.UsersId),
                    r => r.HasOne<RoleEntity>().WithMany().HasForeignKey(e => e.RolesId)
                );

            //IEnumerable<RoleEntity> roles = Enum.GetValues<RoleType>().Select(r => new RoleEntity
            //{
            //    Id = (int)r,
            //    Name = r.ToString()
            //});

            //builder.HasData(roles);
        }
    }
}
