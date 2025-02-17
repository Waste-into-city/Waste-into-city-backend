using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Configurations
{
    public partial class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users");

            builder.Property(u => u.Id).HasColumnName("id");

            builder.Property(u => u.Nickname).IsRequired().HasColumnName("nickname");

            builder.Property(u => u.Email).IsRequired().HasColumnName("email");

            builder.Property(u => u.Password).IsRequired().HasColumnName("password");

            builder.Property(u => u.Ranking).IsRequired().HasColumnName("ranking");

            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasMany(u => u.WorkReportResults)
                .WithOne(w => w.FromParticipant)
                .HasForeignKey("FK_work_report_results_users");

            builder.HasMany(u => u.WorkReportComplaints)
                .WithOne(w => w.FromParticipantUser)
                .HasForeignKey("FK_work_report_complaint_from_users");

            builder.HasMany(u => u.WorkColleagueReportsFrom)
                .WithOne(w => w.UserFromParticipant)
                .HasForeignKey("FK_work_colleague_reports_from_participant_users");

            builder.HasMany(u => u.WorkColleagueReportsAbout)
                .WithOne(w => w.UserAboutColleague)
                .HasForeignKey("FK_work_colleague_reports_about_colleague_users");

            builder.HasMany(u => u.NotificationsFrom)
                .WithOne(n => n.FromUser)
                .HasForeignKey("FK_notifications_from_user");

            builder.HasMany(u => u.NotificationsTo)
                .WithOne(n => n.ToUser)
                .HasForeignKey("FK_notifications_to_user");

            builder.HasMany(u => u.TrashcanPointReports)
                .WithOne(u => u.User)
                .HasForeignKey("FK_trashcan_point_reports_users");
        }
    }
}
