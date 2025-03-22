using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public const string TABLE_NAME = "users";

        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.Property(u => u.Id).HasColumnName("id");

            builder.Property(u => u.Nickname).IsRequired().HasColumnName("nickname").HasMaxLength(User.NICKNAME_LENGTH_MAX);

            builder.Property(u => u.Email).IsRequired().HasColumnName("email").HasMaxLength(User.EMAIL_LENGTH_MAX);

            builder.Property(u => u.Password).IsRequired().HasColumnName("password").HasMaxLength(User.PASSWORD_LENGTH_MAX);

            builder.Property(u => u.Ranking).IsRequired().HasColumnName("ranking");

            builder.Property(u => u.NegativeScore).IsRequired().HasColumnName("negative_score");

            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasMany(u => u.WorkReportResults)
                .WithOne(w => w.FromParticipant)
                .HasForeignKey(w => w.FromParticipantsId);

            builder.HasMany(u => u.WorkReportComplaints)
                .WithOne(w => w.FromUser)
                .HasForeignKey(w => w.FromUsersId);

            builder.HasMany(u => u.WorkColleagueReportsFrom)
                .WithOne(w => w.UserFromParticipant)
                .HasForeignKey(w => w.FromParticipantId);

            builder.HasMany(u => u.WorkColleagueReportsAbout)
                .WithOne(w => w.UserAboutColleague)
                .HasForeignKey(w => w.AboutColleagueId);

            builder.HasMany(u => u.NotificationsFrom)
                .WithOne(n => n.FromUser)
                .HasForeignKey(n => n.FromUsersId);

            builder.HasMany(u => u.NotificationsTo)
                .WithOne(n => n.ToUser)
                .HasForeignKey(n => n.ToUsersId);

            builder.HasMany(u => u.TrashcanPointReports)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UsersId);

            builder.HasMany(u => u.RefreshTokens)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);

            builder.HasMany(u => u.WorkApplications)
                .WithOne(w => w.FromUser)
                .HasForeignKey(w => w.FromUsersId);

            //builder.HasOne(u => u.AccessToken)
            //    .WithOne(a => a.User);
        }
    }
}
