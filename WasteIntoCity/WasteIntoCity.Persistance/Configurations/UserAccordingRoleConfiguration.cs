using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class UserAccordingRoleConfiguration : IEntityTypeConfiguration<UserAccordingRoleEntity>
    {
        public const string TABLE_NAME = "user_according_roles";

        public void Configure(EntityTypeBuilder<UserAccordingRoleEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(t => t.RolesId).HasColumnName("roles_id");

            builder.Property(t => t.UsersId).HasColumnName("users_id");

            builder.HasKey(u => new { u.RolesId, u.UsersId });
        }
    }
}
