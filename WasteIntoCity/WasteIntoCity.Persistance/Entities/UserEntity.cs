namespace WasteIntoCity.Persistance.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Nickname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int Ranking { get; set; }

        public List<WorkReportResultEntity> WorkReportResults { get; set; } = [];

        public ICollection<WorkEntity> Works { get; set; } = [];

        public List<WorkReportComplaintEntity> WorkReportComplaints { get; set; } = [];

        public List<WorkColleagueReportEntity> WorkColleagueReportsFrom { get; set; } = [];

        public List<WorkColleagueReportEntity> WorkColleagueReportsAbout { get; set; } = [];

        public ICollection<RoleEntity> Roles { get; set; } = [];

        public List<NotificationEntity> NotificationsFrom { get; set; } = [];

        public List<NotificationEntity> NotificationsTo { get; set; } = [];

        public List<TrashcanPointReportEntity> TrashcanPointReports { get; set; } = [];

        //public AccessTokenEntity? AccessToken { get; set; }

        public List<RefreshTokenEntity> RefreshTokens { get; set; } = [];

        public List<WorkApplicationEntity> WorkApplications { get; set; } = [];
    }
}
