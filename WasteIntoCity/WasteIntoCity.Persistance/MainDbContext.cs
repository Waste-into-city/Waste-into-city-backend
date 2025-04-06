using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information) // Логирование запросов в консоль
                .EnableSensitiveDataLogging()                   // Для детального логирования
                .EnableDetailedErrors();                        // Показывает больше данных при ошибках
        }

        //public DbSet<AccessTokenEntity> AccessTokens { get; set; }

        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        public DbSet<ScoreSettingsTypeEntity> ScoreSettingsTypes { get; set; }

        public DbSet<ImageEntity> Images { get; set; }

        public DbSet<NotificationEntity> Notifications { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<TrashcanEntity> Trashcans { get; set; }

        public DbSet<TrashcanOccupancyTypeEntity> TrashcanOccupancyTypes { get; set; }

        public DbSet<CoordinatesEntity> Coordinates { get; set; }

        public DbSet<TrashcanPointReportEachMarkEntity> TrashcanPointReportEachMarkSet { get; set; }

        public DbSet<TrashcanPointReportEntity> TrashcanPointReports { get; set; }

        public DbSet<TrashcanTypeEntity> TrashcanTypes { get; set; }

        public DbSet<UserAccordingRoleEntity> UserAccordingRoles { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<WorkApplicationEntity> WorkApplications { get; set; }

        public DbSet<WorkColleagueReportEntity> WorkColleagueReports { get; set; }

        public DbSet<WorkComplexityTypeEntity> WorkComplexityTypes { get; set; }

        public DbSet<WorkEntity> Works { get; set; }

        public DbSet<WorkMarkTypeEntity> WorkMarkTypes { get; set; }

        public DbSet<WorkParticipantEntity> WorkParticipants { get; set; }

        public DbSet<WorkReportComplaintEntity> WorkReportComplaints { get; set; }

        public DbSet<WorkReportStatusTypeEntity> WorkReportStatusTypes { get; set; }

        public DbSet<WorkReportResultEntity> WorkReportResults { get; set; }

        public DbSet<WorkStatusTypeEntity> WorkStatusTypes { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Remove(typeof(CascadeDeleteConvention));
            configurationBuilder.Conventions.Remove(typeof(SqlServerOnDeleteConvention));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
        }
    }
}
