namespace WasteIntoCity.Core.Models
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Nickname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public List<WorkReportResultEntity> WorkReportResults { get; set; } = [];

        public ICollection<WorkEntity> Works { get; set; } = [];

        public List<WorkReportComplaintEntity> WorkReportComplaints { get; set; } = [];

        public List<WorkColleagueReportEntity> WorkColleagueReports { get; set; } = [];

        public ICollection<RoleEntity> Roles { get; set; } = [];
    }
}
