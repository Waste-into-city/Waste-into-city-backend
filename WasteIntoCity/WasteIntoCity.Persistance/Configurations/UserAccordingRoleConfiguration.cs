using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class UserAccordingRoleConfiguration : IEntityTypeConfiguration<UserAccordingRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserAccordingRoleEntity> builder)
        {
            builder.ToTable("user_according_roles");

            builder.Property(t => t.RolesId).HasColumnName("roles_id");

            builder.Property(t => t.UsersId).HasColumnName("users_id");

            builder.HasKey(u => new { u.RolesId, u.UsersId });
        }
    }
}
